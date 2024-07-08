
namespace Security.Sample.Api;

partial class ApiFuncs
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    AddSecrets(builder.Configuration);
    AddSettings(builder.Configuration);
    AddEnvironmentVariables(builder.Configuration);
    AddCommandLine(builder.Configuration, args);

    var encryptionKeysPath = GetEncryptionKeysPath(Environment.CurrentDirectory);
    var corsOrigin = GetCorsOrigins(builder.Configuration);
    AddServices(builder.Services, builder.Configuration, encryptionKeysPath, corsOrigin);

    var app = builder.Build();
    UseMiddlewares(app);
    MapEndpoints(app);

    LogApplicationStart(app);
    app.Run();
  }
}