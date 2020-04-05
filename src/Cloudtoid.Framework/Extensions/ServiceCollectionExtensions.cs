namespace Cloudtoid
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;

    // Added this class so that the extensions all return the service collection
    [DebuggerStepThrough]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection TryAddScoped<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddScoped(services, implementationFactory);
            return services;
        }

        public static IServiceCollection TryAddScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceCollectionDescriptorExtensions.TryAddScoped<TService, TImplementation>(services);
            return services;
        }

        public static IServiceCollection TryAddScoped(this IServiceCollection services, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            ServiceCollectionDescriptorExtensions.TryAddScoped(services, service, implementationFactory);
            return services;
        }

        public static IServiceCollection TryAddScoped<TService>(this IServiceCollection services) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddScoped<TService>(services);
            return services;
        }

        public static IServiceCollection TryAddScoped(this IServiceCollection services, Type service)
        {
            ServiceCollectionDescriptorExtensions.TryAddScoped(services, service);
            return services;
        }

        public static IServiceCollection TryAddScoped(this IServiceCollection services, Type service, Type implementationType)
        {
            ServiceCollectionDescriptorExtensions.TryAddScoped(services, service, implementationType);
            return services;
        }

        public static IServiceCollection TryAddSingleton(this IServiceCollection services, Type service)
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton(services, service);
            return services;
        }

        public static IServiceCollection TryAddSingleton(this IServiceCollection services, Type service, Type implementationType)
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton(services, service, implementationType);
            return services;
        }

        public static IServiceCollection TryAddSingleton(this IServiceCollection services, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton(services, service, implementationFactory);
            return services;
        }

        public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection services) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton<TService>(services);
            return services;
        }

        public static IServiceCollection TryAddSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton<TService, TImplementation>(services);
            return services;
        }

        public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection services, TService instance) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton(services, instance);
            return services;
        }

        public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddSingleton(services, implementationFactory);
            return services;
        }

        public static IServiceCollection TryAddTransient<TService>(this IServiceCollection services, Func<IServiceProvider, TService> implementationFactory) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddTransient(services, implementationFactory);
            return services;
        }

        public static IServiceCollection TryAddTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            ServiceCollectionDescriptorExtensions.TryAddTransient<TService, TImplementation>(services);
            return services;
        }

        public static IServiceCollection TryAddTransient<TService>(this IServiceCollection services) where TService : class
        {
            ServiceCollectionDescriptorExtensions.TryAddTransient<TService>(services);
            return services;
        }

        public static IServiceCollection TryAddTransient(this IServiceCollection services, Type service, Func<IServiceProvider, object> implementationFactory)
        {
            ServiceCollectionDescriptorExtensions.TryAddTransient(services, service, implementationFactory);
            return services;
        }

        public static IServiceCollection TryAddTransient(this IServiceCollection services, Type service, Type implementationType)
        {
            ServiceCollectionDescriptorExtensions.TryAddTransient(services, service, implementationType);
            return services;
        }

        public static IServiceCollection TryAddTransient(this IServiceCollection services, Type service)
        {
            ServiceCollectionDescriptorExtensions.TryAddTransient(services, service);
            return services;
        }

        public static bool Exists<TService, TImplementation>(this IServiceCollection services)
        {
            return services.Any(s => s.ServiceType == typeof(TService) && s.ImplementationType == typeof(TImplementation));
        }

        public static bool Exists<TService>(this IServiceCollection services)
        {
            return services.Any(s => s.ServiceType == typeof(TService));
        }
    }
}
