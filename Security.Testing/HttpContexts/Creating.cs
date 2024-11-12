using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Time.Testing;

namespace Security.Testing;

partial class Funcs
{
  public static HttpContext CreateHttpContext()
  {
    var services = new ServiceCollection()
      .AddSingleton<TimeProvider, FakeTimeProvider>()
      .AddDataProtection($"{Environment.CurrentDirectory}/keys")
      .BuildServiceProvider();
    return new DefaultHttpContext() { RequestServices = services };
  }

  public static HttpContext CreateHttpContext(Uri uri)
  {
    var context = CreateHttpContext();
    context.Request.Scheme = uri.Scheme;
    context.Request.Host = new HostString(uri.Host);
    context.Request.Path = uri.AbsolutePath;
    context.Request.Query = new QueryCollection(QueryHelpers.ParseQuery(uri.Query));
    return context;
  }
}

