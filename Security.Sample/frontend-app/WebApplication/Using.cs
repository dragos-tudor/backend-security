
using System;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;

namespace Security.Sample.App;

partial class AppFuncs
{
  static IApplicationBuilder UseMiddlewares (WebApplication app, ConfigurationManager configuration, DateTime currentDate) =>
    app
      .UseDefaultFiles()
      .UseStaticFiles(new StaticFileOptions() {
          HttpsCompression = HttpsCompressionMode.Compress,
          OnPrepareResponse = (context) =>
            SetResponseCache(context.Context.Response, currentDate, GetResponseCacheSettings(configuration).IntervalSeconds)
      });
}