
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Time.Testing;

namespace Security.Testing;

partial class Funcs {

  public static WebApplication CreateHttpServer (Action<IServiceCollection>? configServices = default, LogLevel minimumLogLevel = LogLevel.None) {
    var builder = WebApplication.CreateBuilder();
    configServices?.Invoke(builder.Services);
    builder.Services
      .AddLogging(o => o.SetMinimumLevel(minimumLogLevel))
      .AddDataProtection(Environment.CurrentDirectory + "/keys")
      .AddSingleton<TimeProvider>(new FakeTimeProvider());
    builder.WebHost.UseTestServer();

    return builder
      .Build()
      .SkipUsingAuthenticationMiddlewares()
      .SkipUsingAuthorizationMiddlewares();
  }

}

