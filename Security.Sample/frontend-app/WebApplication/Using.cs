
using System;
using Microsoft.Extensions.Configuration;

namespace Security.Sample.App;

partial class AppFuncs
{
  static IApplicationBuilder UseMiddlewares (
    WebApplication app,
    ConfigurationManager configuration,
    DateTime currentDate)
  =>
    app
      .UseDefaultFiles()
      .Use(async (context, next) => {
        if(IsRouteRequest(context.Request)) await SendFileResponse(context.Response, "wwwroot/index.html", context.RequestAborted);
        if(IsRouteRequest(context.Request)) return;
        await next();
      })
      .UseStaticFiles(new StaticFileOptions() {
        OnPrepareResponse = (context) =>
          SetResponseCache(context.Context.Response, currentDate, GetResponseCacheInterval(configuration))
      });
}