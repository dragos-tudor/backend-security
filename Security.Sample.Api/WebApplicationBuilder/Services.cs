using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Security.Samples;

partial class SampleFuncs {

  internal static IServiceCollection AddServices(WebApplicationBuilder app) =>
    app.Services
      .AddCookies()
      .AddGoogle(SetGoogleOptions(app))
      .AddFacebook(SetFacebookOptions(app))
      .AddTwitter(SetTwitterOptions(app))
      .AddLogging(o => o.SetMinimumLevel(LogLevel.Warning))
      .AddDataProtection()
      .PersistKeysToFileSystem(new (Environment.CurrentDirectory + "/bin/keys"))
      .Services;

}