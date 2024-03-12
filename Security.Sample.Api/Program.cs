
namespace Security.Sample.Api;

partial class SampleFuncs
{
  public static void Main(string[] args)
  {
    var appBuilder = WebApplication.CreateBuilder(args);
    AddSecrets(appBuilder);
    AddSettings(appBuilder).Build();
    AddEnvironmentVariables(appBuilder);
    AddCommandLine(appBuilder, args);

    var keysPath = GetKeysPath(Environment.CurrentDirectory);
    var corsOrigin = GetCorsOrigins(appBuilder);
    AddServices(appBuilder, keysPath, corsOrigin);

    var app = appBuilder.Build();
    UseMiddlewares(app);
    MapEndpoints(app);

    LogAppStart(appBuilder);
    app.Run();
  }
}