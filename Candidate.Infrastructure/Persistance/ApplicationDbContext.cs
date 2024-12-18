using Candidate.Application.Interfaces.Data;
using Candidate.Application.Settings;
using Candidate.Domain.Common;
using Candidate.Domain.Entities;
using Candidate.Helper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace Candidate.Infrastructure.Persistance
{
    public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public DbSet<Candidates> Candidates { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var basePath = AppContext.BaseDirectory;

            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            if (!Directory.Exists(basePath))
            {
                throw new DirectoryNotFoundException($"The directory '{basePath}' does not exist.");
            }

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .Build();

            var databaseSettings = new DataBaseSetting();

            configuration.GetSection("DataBaseSetting").Bind(databaseSettings);

            var connectionString = databaseSettings.DbProvider == Constants.DbProviderKeys.Npgsql
                ? databaseSettings.NpgSqlConnectionString
                : databaseSettings.SqlServerConnectionString;

            optionsBuilder = optionsBuilder.UseDatabase(databaseSettings.DbProvider, connectionString!);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
