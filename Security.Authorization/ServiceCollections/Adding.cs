
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  public static IServiceCollection AddAuthorizationServices (this IServiceCollection services) =>
    services
      .AddAuthorization()
      .AddKeyedSingleton(CategoryNameLogger, (services, serviceKey) => CreateLogger(services, (string)serviceKey));
}