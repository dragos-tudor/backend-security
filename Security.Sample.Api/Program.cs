
// Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
using static System.Console;

var builder = CreateWebApplicationBuilder(args);
AddConfigurationProviders(builder);
AddServices(builder);

var app = builder.Build();
UseMiddlewares(app);
MapEndpoints(app);

WriteLine($"Server started: {builder.WebHost.GetSetting("Kestrel:Endpoints:Https:Url")}");
WriteLine($"  Environment: {builder.Environment.EnvironmentName}");
WriteLine($"  Content directory: {builder.Environment.ContentRootPath}");
app.Run();