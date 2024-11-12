
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Security.Testing;

partial class Funcs
{
  public static WebApplication CreateHttpServer(
    Action<IServiceCollection>? configServices = default,
    LogLevel minimumLogLevel = LogLevel.None)
  {
    var builder = WebApplication.CreateBuilder();
    configServices?.Invoke(builder.Services);
    builder.Services.AddTestServices(minimumLogLevel);
    builder.WebHost.UseTestServer();

    return builder
      .Build()
      .SkipUsingAuthenticationMiddlewares()
      .SkipUsingAuthorizationMiddlewares();
  }
}

