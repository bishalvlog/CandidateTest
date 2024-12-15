using Candidate.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Candidate.Application.Settings
{
    public class DataBaseSetting : IValidatableObject
    {
        public string DbProvider {  get; set; }  

        public string ? NpgSqlConnectionString { get; set; }

        public string? SqlServerConnectionString { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(DbProvider))
            {
              yield  return new ValidationResult(
                    $"{nameof(DataBaseSetting)}. {nameof(DbProvider)} is not configure",
                    new[] {nameof(DbProvider)});
            }

            if (string.IsNullOrEmpty(NpgSqlConnectionString) && string.IsNullOrEmpty(SqlServerConnectionString))
            {
                yield return new ValidationResult(
                    $"{nameof(DataBaseSetting)}. A valid connection string for {GetDbProviderKey(DbProvider)} is not configured.");
            }
        }

        private static string GetDbProviderKey(string dbProvider)
        {
            return dbProvider switch
            {
                Constants.DbProviderKeys.Npgsql => "PostgreSQL",
                Constants.DbProviderKeys.SqlServer => "SQL Service",
                _ => ""
            };
        }
    }
}
