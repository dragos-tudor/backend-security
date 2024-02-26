
namespace Security.Sample.Api;

partial class SampleFuncs
{
  public static void Main(string[] args)
  {
    var appBuilder = CreateWebApplicationBuilder(args);
    var keysPath = Environment.CurrentDirectory + "/bin/keys";
    AddConfigurations(appBuilder);
    AddSecrets(appBuilder, args);
    AddServices(appBuilder, keysPath, GetCorsOrigins(appBuilder));

    var app = appBuilder.Build();
    UseMiddlewares(app);
    MapEndpoints(app);

    LogAppStart(appBuilder);
    app.Run();
  }
}