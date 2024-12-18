using Candidate.Application.Interfaces.Services;
using CandidateWeb.Dependency;
using CandidateWeb.Models.Application;
using CandidateWeb.Service.HTTP;
using CandidateWeb.Service.Implementation;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace CandidateClient.Dependency
{
    public static class InfrastructureService
    {
        public static void AddInfrastructureService(this IServiceCollection services,
         WebAssemblyHostBuilder builder)
        {
            var configuration = builder.Configuration;

            var environment = builder.HostEnvironment;

            Console.WriteLine($"Environment: {builder.HostEnvironment.Environment}.");
            Console.WriteLine($"Base Address: {builder.HostEnvironment.BaseAddress}.");

            configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.Environment}.json", optional: true, reloadOnChange: true);

            var applicationConfiguration = configuration.GetSection("Configuration").Get<Configuration>()
                ?? throw new KeyNotFoundException("The application configuration could not be found, please try again.");

            services.AddDependencyServices();

            services.AddScoped(_ => new ApiHttpClient(new HttpClient(), applicationConfiguration.ApiUrl));

            services.AddScoped(_ => new LocalHttpClient(new HttpClient(), builder.HostEnvironment.BaseAddress));

            services.AddAuthorizationCore();

            services.AddMudServices();
        }
    }
}
