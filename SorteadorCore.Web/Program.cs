using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

using SorteadorCore.Domain.Interfaces.Services;
using SorteadorCore.Domain.Services;
using SorteadorCore.Domain.Interfaces.Repository;
using SorteadorCore.Infra.Repositories;
using SorteadorCore.Infra.Context;
using SorteadorCore.Application;
using SorteadorCore.Application.Interfaces;
using SorteadorCore.Web.Hubs;
using SorteadorCore.Web.AutoMapper;

namespace SorteadorCore.Web
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
                })
                .ConfigureServices(services =>
                {

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
                    services.AddTransient<IUsuarioService, UsuarioService>();
                    services.AddTransient<ILoginService, LoginService>();

                    services.AddTransient<ISorteioRepository, SorteioRepository>();
                    services.AddTransient<ISorteioDetalheRepository, SorteioDetalheRepository>();
                    services.AddTransient<ISalaRepository, SalaRepository>();
                    services.AddTransient<IParticipanteRepository, ParticipanteRepository>();
                    services.AddTransient<IUsuarioRepository, UsuarioRepository>();

                    services.AddTransient<ISorteioAppService, SorteioAppService>();
                    services.AddTransient<ISorteioDetalheAppService, SorteioDetalheAppService>();
                    services.AddTransient<ISalaAppService, SalaAppService>();
                    services.AddTransient<IParticipanteAppService, ParticipanteAppService>();
                    services.AddTransient<ILoginAppService, LoginAppService>();
                    services.AddMvc();
                    services.AddSignalR();

                    
                    services.AddAuthentication("TestScheme")
                    .AddCookie("TestScheme", options =>
                    {
                        //options.CookieHttpOnly = true;
                        options.LoginPath = "/Login";
                    });

                    //services.AddSingleton<IConfigureOptions<CookieAuthenticationOptions>, ConfigureCookies>();
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
                    app.UseAuthentication();

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

                })
                .Build();

            host.Run();
        }
    }
}
