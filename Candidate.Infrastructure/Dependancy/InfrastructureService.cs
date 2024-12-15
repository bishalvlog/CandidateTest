using Candidate.Application.Interfaces.Data;
using Candidate.Application.Settings;
using Candidate.Domain.Common;
using Candidate.Helper;
using Candidate.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Infrastructure.Dependancy
{
    public static class InfrastructureService
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = new DataBaseSetting();

            configuration.GetSection(nameof(DataBaseSetting)).Bind(databaseSettings);

            var connectionString = databaseSettings.DbProvider == Constants.DbProviderKeys.Npgsql
             ? databaseSettings.NpgSqlConnectionString
             : databaseSettings.SqlServerConnectionString;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseDatabase(databaseSettings.DbProvider, connectionString!);
            });

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetService<ApplicationDbContext>()!);

          // services.AddHttp();

            services.AddDistributedMemoryCache();

            EnsureDatabaseMigrated(services);

            return services;
        }

        private static void EnsureDatabaseMigrated(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();

            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();   
        }
    }
}
