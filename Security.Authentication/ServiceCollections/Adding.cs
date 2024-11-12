
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static IServiceCollection AddSingleton<TService>(this IServiceCollection services, TService? service) where TService : class => service is null? services: ServiceCollectionServiceExtensions.AddSingleton(services, service);

  public static IServiceCollection TryAddSingleton<TService>(this IServiceCollection services, TService service) where TService : class { ServiceCollectionDescriptorExtensions.TryAddSingleton(services, service); return services; }
}