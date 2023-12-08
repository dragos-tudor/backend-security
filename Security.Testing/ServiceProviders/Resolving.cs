
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Testing;

partial class Funcs {

  public static T? ResolveService<T> (HttpContext context) where T : notnull => ResolveService<T>(context.RequestServices);

  public static T? ResolveService<T> (IServiceProvider services) where T : notnull => services.GetService<T>();

  public static T ResolveRequiredService<T> (HttpContext context) where T : notnull => ResolveRequiredService<T>(context.RequestServices);

  public static T ResolveRequiredService<T> (IServiceProvider services) where T : notnull => services.GetRequiredService<T>();

}