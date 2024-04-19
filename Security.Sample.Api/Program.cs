using Microsoft.Extensions.Logging;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    var configuration = builder.Configuration;
    var services = builder.Services;

    AddSecrets(configuration);
    AddSettings(configuration);
    AddEnvironmentVariables(configuration);
    AddCommandLine(configuration, args);

    var encryptionKeysPath = GetEncryptionKeysPath(Environment.CurrentDirectory);
    var corsOrigin = GetCorsOrigins(configuration);
    AddServices(services, configuration, encryptionKeysPath, corsOrigin);

    var app = builder.Build();
    SetLoggerFactory(ResolveService<ILoggerFactory>(app.Services)!);
    LogAppStart(builder);

    UseMiddlewares(app);
    MapEndpoints(app);
    app.Run();
  }
}