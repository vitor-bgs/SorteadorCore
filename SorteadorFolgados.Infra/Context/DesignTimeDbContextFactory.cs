using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace SorteadorFolgados.Infra.Context
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<BancoContexto>
    {
        public BancoContexto CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<BancoContexto>();
            var connectionString = configuration.GetConnectionString("BancoSorteadorString");
            builder.UseNpgsql(connectionString);
            return new BancoContexto(builder.Options);
        }
    }
}
