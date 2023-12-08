
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Security.Authentication;

partial class Funcs {

  public static IServiceCollection TryAddSingleton<TService, TImplementation> (this IServiceCollection services)
    where TService : class
    where TImplementation : class, TService {
      ServiceCollectionDescriptorExtensions.TryAddSingleton<TService, TImplementation>(services); return services;
    }

  public static IServiceCollection TryAddSingleton<TService> (this IServiceCollection services, TService instance)
    where TService : class {
      ServiceCollectionDescriptorExtensions.TryAddSingleton<TService>(services, instance); return services;
    }

}