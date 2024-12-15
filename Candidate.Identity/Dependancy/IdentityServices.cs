using Candidate.Application.Settings;
using Candidate.Domain.Common;
using Candidate.Helper;
using Candidate.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Candidate.Identity.Dependancy
{
    public static class IdentityServices
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseSettings = new DataBaseSetting();

            configuration.GetSection("DataBaseSetting").Bind(databaseSettings);

            var connectionString = databaseSettings.DbProvider == Constants.DbProviderKeys.Npgsql
                ? databaseSettings.NpgSqlConnectionString
                : databaseSettings.SqlServerConnectionString;

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseDatabase(databaseSettings.DbProvider, connectionString!);
            });

            services.Configure<IdentityOptions>(options =>
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);

            services.AddHttpContextAccessor();

            services.AddAuthorization();

            return services;
        }
    }
}
