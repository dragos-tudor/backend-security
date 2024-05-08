
using System;

namespace Security.Sample.App;

partial class AppFuncs
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    AddSettings(builder.Configuration);
    AddEnvironmentVariables(builder.Configuration);
    AddCommandLine(builder.Configuration, args);
    AddServices(builder.Services);

    var app = builder.Build();
    UseMiddlewares(app, builder.Configuration, DateTime.Now);
    LogApplicationStart(app);
    app.Run();
  }
}


