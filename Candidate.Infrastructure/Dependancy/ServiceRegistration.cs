﻿using Microsoft.Extensions.DependencyInjection;

namespace Candidate.Infrastructure.Dependancy
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDependencyServices(this IServiceCollection services) => 
            services
            .AddServices(typeof(IServiceScope), ServiceLifetime.Transient)
            .AddServices(typeof(IServiceScope), ServiceLifetime.Scoped);

        private static IServiceCollection AddServices(this IServiceCollection services, Type interfaceType, ServiceLifetime lifetime)
        {
            var interfaceTypes =
                AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(t => interfaceType.IsAssignableFrom(t) && t is { IsClass: true, IsAbstract: false })
                    .Select(t => new
                    {
                        Service = t.GetInterfaces().FirstOrDefault(),
                        Implementation = t
                    })
                    .Where(t => t.Service is not null && interfaceType.IsAssignableFrom(t.Service));

            foreach (var type in interfaceTypes)
            {
                services.AddService(type.Service!, type.Implementation, lifetime);
            }

            return services;
        }

       private static IServiceCollection AddService(this IServiceCollection services,
       Type serviceType,
       Type implementationType,
       ServiceLifetime lifetime) =>
       lifetime switch
       {
           ServiceLifetime.Scoped => services.AddScoped(serviceType, implementationType),
           ServiceLifetime.Transient => services.AddTransient(serviceType, implementationType),
           _ => throw new ArgumentException("Invalid lifeTime", nameof(lifetime))
       };
    }
}