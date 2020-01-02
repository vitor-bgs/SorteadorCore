using Google.Cloud.Diagnostics.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using AutoMapper;
using SorteadorFolgados.AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using SorteadorFolgados.Domain.Interfaces.Services;
using SorteadorFolgados.Domain.Services;
using SorteadorFolgados.Infra.Repositories;
using SorteadorFolgados.Domain.Interfaces.Repository;
using SorteadorFolgados.Infra.Context;
using Microsoft.EntityFrameworkCore;
using SorteadorFolgados.Application;
using SorteadorFolgados.Application.Interfaces;
using SorteadorFolgados.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SorteadorFolgados
{
    public class Program
    {
        public static IHostingEnvironment HostingEnvironment { get; private set; }
        public static IConfiguration Configuration { get; private set; }

        public static string GcpProjectId { get; private set; }
        public static bool HasGcpProjectId => !string.IsNullOrEmpty(GcpProjectId);

        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    HostingEnvironment = context.HostingEnvironment;

                    configBuilder.SetBasePath(HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{HostingEnvironment.EnvironmentName}.json", optional: true)
                        .AddEnvironmentVariables();

                    Configuration = configBuilder.Build();
                    GcpProjectId = GetProjectId(Configuration);
                })
                .ConfigureServices(services =>
                {
                    // Add framework services.Microsoft.VisualStudio.ExtensionManager.ExtensionManagerService
                    //services.AddMvc();

                    if (HasGcpProjectId)
                    {
                        // Enables Stackdriver Trace.
                        services.AddGoogleTrace(options => options.ProjectId = GcpProjectId);
                        // Sends Exceptions to Stackdriver Error Reporting.
                        services.AddGoogleExceptionLogging(
                            options =>
                            {
                                options.ProjectId = GcpProjectId;
                                options.ServiceName = GetServiceName(Configuration);
                                options.Version = GetVersion(Configuration);
                            });
                        services.AddSingleton<ILoggerProvider>(sp => GoogleLoggerProvider.Create(GcpProjectId));
                    }

                    //Banco de Dados
                    services.AddEntityFrameworkNpgsql().AddDbContext<BancoContexto>(options =>
                    {
                        options.UseNpgsql(Configuration.GetConnectionString("BancoSorteadorString"));
                    });


                    //AutoMapper
                    var mappingConfig = new MapperConfiguration(mc => 
                    {
                        mc.AddProfile(new DomainToViewProfile());
                        mc.AddProfile(new ViewToDomainProfile());
                    });
                    IMapper mapper = mappingConfig.CreateMapper();
                    services.AddSingleton(mapper);

                    //Injeção de Dependência
                    services.AddTransient<ISorteioService, SorteioService>();
                    services.AddTransient<ISorteioDetalheService, SorteioDetalheService>();
                    services.AddTransient<ISalaService, SalaService>();
                    services.AddTransient<IParticipanteService, ParticipanteService>();

                    services.AddTransient<ISorteioRepository, SorteioRepository>();
                    services.AddTransient<ISorteioDetalheRepository, SorteioDetalheRepository>();
                    services.AddTransient<ISalaRepository, SalaRepository>();
                    services.AddTransient<IParticipanteRepository, ParticipanteRepository>();

                    services.AddTransient<ISorteioAppService, SorteioAppService>();
                    services.AddTransient<ISorteioDetalheAppService, SorteioDetalheAppService>();
                    services.AddTransient<ISalaAppService, SalaAppService>();
                    services.AddTransient<IParticipanteAppService, ParticipanteAppService>();
                    services.AddMvc();
                    services.AddSignalR();

                    services.Configure<CookiePolicyOptions>(options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

                    services.AddDefaultIdentity<ViewModel.UsuarioViewModel>();

                    //services.Configure<IdentityOptions>(options =>
                    //{
                    //    options.
                    //});

                    services.ConfigureApplicationCookie(options =>
                    {
                        options.Cookie.HttpOnly = true;
                        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                        options.LoginPath = "/Login/Index";
                        options.AccessDeniedPath = "/Login/Index";
                        options.SlidingExpiration = true;
                    });
                    services.AddAuthentication("TestScheme")
                    .AddCookie("TestScheme", options =>
                    {
                        options.CookieHttpOnly = true;
                        options.LoginPath = "/Login";
                    });
                })
                .ConfigureLogging(loggingBuilder =>
                {
                    loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                    if (HostingEnvironment.IsDevelopment())
                    {
                        // Only use Console and Debug logging during development.
                        loggingBuilder.AddConsole(options =>
                            options.IncludeScopes = Configuration.GetValue<bool>("Logging:IncludeScopes"));
                        loggingBuilder.AddDebug();
                    }
                })
                .Configure((app) =>
                {
                    var logger = app.ApplicationServices.GetService<ILoggerFactory>().CreateLogger("Startup");
                    if (HasGcpProjectId)
                    {
                        // Sends logs to Stackdriver Error Reporting.
                        app.UseGoogleExceptionLogging();
                        // Sends logs to Stackdriver Trace.
                        app.UseGoogleTrace();

                        logger.LogInformation(
                            "Stackdriver Logging enabled: https://console.cloud.google.com/logs/");
                        logger.LogInformation(
                            "Stackdriver Error Reporting enabled: https://console.cloud.google.com/errors/");
                        logger.LogInformation(
                            "Stackdriver Trace enabled: https://console.cloud.google.com/traces/");
                    }
                    else
                    {
                        logger.LogWarning(
                            "Stackdriver Logging not enabled. Missing Google:ProjectId in configuration.");
                        logger.LogWarning(
                            "Stackdriver Error Reporting not enabled. Missing Google:ProjectId in configuration.");
                        logger.LogWarning(
                            "Stackdriver Trace not enabled. Missing Google:ProjectId in configuration.");
                    }

                    if (HostingEnvironment.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseStaticFiles(new StaticFileOptions
                        {
                            FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
                            RequestPath = new PathString("/lib")
                        });
                    }
                    else
                    {
                        app.UseExceptionHandler("/Home/Error");
                    }

                    app.UseForwardedHeaders(new ForwardedHeadersOptions
                    {
                        ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                        ForwardedHeaders.XForwardedProto
                    });

                    app.UseStaticFiles();

                    app.UseMvc(routes =>
                    {
                        routes.MapRoute(
                            name: "default",
                            template: "{controller=Sorteios}/{action=Index}/{id?}");
                    });

                    app.UseSignalR(routes =>
                    {
                        routes.MapHub<SorteioHub>("/sorteio");
                    });

                    app.UseAuthentication();
                })
                .Build();

            host.Run();
        }

        /// <summary>
        /// Get the Google Cloud Platform Project ID from the platform it is running on,
        /// or from the appsettings.json configuration if not running on Google Cloud Platform.
        /// </summary>
        /// <param name="config">The appsettings.json configuration.</param>
        /// <returns>
        /// The ID of the GCP Project this service is running on, or the Google:ProjectId
        /// from the configuration if not running on GCP.
        /// </returns>
        private static string GetProjectId(IConfiguration config)
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var projectId = instance?.ProjectId ?? config["Google:ProjectId"];
            if (string.IsNullOrEmpty(projectId))
            {
                // Set Google:ProjectId in appsettings.json to enable stackdriver logging outside of GCP.
                return null;
            }
            return projectId;
        }

        /// <summary>
        /// Gets a service name for error reporting.
        /// </summary>
        /// <param name="config">The appsettings.json configuration to read a service name from.</param>
        /// <returns>
        /// The name of the Google App Engine service hosting this application,
        /// or the Google:ErrorReporting:ServiceName configuration field if running elsewhere.
        /// </returns>
        /// <seealso href="https://cloud.google.com/error-reporting/docs/formatting-error-messages#FIELDS.service"/>
        private static string GetServiceName(IConfiguration config)
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var serviceName = instance?.GaeDetails?.ServiceId ?? config["Google:ErrorReporting:ServiceName"];
            if (string.IsNullOrEmpty(serviceName))
            {
                throw new InvalidOperationException(
                    "The error reporting library requires a service name. " +
                    "Update appsettings.json by setting the Google:ErrorReporting:ServiceName property with your " +
                    "Service Id, then recompile.");
            }
            return serviceName;
        }

        /// <summary>
        /// Gets a version id for error reporting.
        /// </summary>
        /// <param name="config">The appsettings.json configuration to read a version id from.</param>
        /// <returns>
        /// The version of the Google App Engine service hosting this application,
        /// or the Google:ErrorReporting:Version configuration field if running elsewhere.
        /// </returns>
        /// <seealso href="https://cloud.google.com/error-reporting/docs/formatting-error-messages#FIELDS.version"/>
        private static string GetVersion(IConfiguration config)
        {
            var instance = Google.Api.Gax.Platform.Instance();
            var versionId = instance?.GaeDetails?.VersionId ?? config["Google:ErrorReporting:Version"];
            if (string.IsNullOrEmpty(versionId))
            {
                throw new InvalidOperationException(
                    "The error reporting library requires a version id. " +
                    "Update appsettings.json by setting the Google:ErrorReporting:Version property with your " +
                    "service version id, then recompile.");
            }
            return versionId;
        }
    }
}
