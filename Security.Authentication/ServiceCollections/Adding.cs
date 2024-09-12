
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static IServiceCollection AddSingleton<TService> (this IServiceCollection services, TService? service) where TService : class => service is null? services: ServiceCollectionServiceExtensions.AddSingleton(services, service);
}