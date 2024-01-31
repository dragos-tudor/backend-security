
using static System.Console;

namespace Security.Sample.Api;

public class Program
{
  public static void Main(string[] args)
  {
    // Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
    var builder = CreateWebApplicationBuilder(args);
    AddConfigurationProviders(builder);
    AddServices(builder, Environment.CurrentDirectory + "/bin/keys");

    var app = builder.Build();
    UseMiddlewares(app);
    MapEndpoints(app);

    WriteLine($"Server started: {builder.WebHost.GetSetting("Kestrel:Endpoints:Https:Url")}");
    WriteLine($"  Environment: {builder.Environment.EnvironmentName}");
    WriteLine($"  Content directory: {builder.Environment.ContentRootPath}");
    app.Run();
  }
}