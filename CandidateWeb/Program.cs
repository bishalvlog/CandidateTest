using CandidateClient.Dependency;
using CandidateWeb;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

var rootComponents = builder.RootComponents;

rootComponents.Add<App>("#app");

rootComponents.Add<HeadOutlet>("head::after");

var service = builder.Services;

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

service.AddInfrastructureService(builder);

await builder.Build().RunAsync();
