
namespace Security.Sample.Api;

partial class SampleFuncs
{
  public static void Main(string[] args)
  {
    // Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
    var appBuilder = CreateWebApplicationBuilder(args);
    AddConfigurations(appBuilder);
    AddServices(appBuilder, Environment.CurrentDirectory + "/bin/keys");

    var app = appBuilder.Build();
    UseMiddlewares(app);
    MapEndpoints(app);

    LogAppInfo(appBuilder);
    app.Run();
  }
}