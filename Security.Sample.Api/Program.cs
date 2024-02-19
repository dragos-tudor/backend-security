
namespace Security.Sample.Api;

partial class SampleFuncs
{
  public static void Main(string[] args)
  {
    var appBuilder = CreateWebApplicationBuilder(args);
    var keysPath = Environment.CurrentDirectory + "/bin/keys";
    AddConfigurations(appBuilder);
    AddSecrets(appBuilder);
    AddServices(appBuilder, keysPath);

    var app = appBuilder.Build();
    UseMiddlewares(app);
    MapEndpoints(app);

    LogAppInfo(appBuilder);
    app.Run();
  }
}