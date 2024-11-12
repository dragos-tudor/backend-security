
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static TService? ResolveKeyedService<TService>(IServiceProvider services, object serviceKey) where TService : class => services.GetKeyedService<TService>(serviceKey);

  public static TService ResolveRequiredService<TService>(HttpContext context) where TService : class => ResolveRequiredService<TService>(context.RequestServices);

  public static TService ResolveRequiredService<TService>(IServiceProvider services) where TService : class => services.GetRequiredService<TService>();

  public static TService? ResolveService<TService>(IServiceProvider services) where TService : class => services.GetService<TService>();
}