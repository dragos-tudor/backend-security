using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Security.Samples;

partial class SampleFuncs {

  internal static IServiceCollection AddServices(WebApplicationBuilder app, string keysPath) =>
    app.Services
      .AddCookies(SetCookieOptions())
      .AddGoogle(SetGoogleOptions(app))
      .AddFacebook(SetFacebookOptions(app))
      .AddTwitter(SetTwitterOptions(app))
      .AddAuthorizationCore()
      .AddLogging(o => o.SetMinimumLevel(LogLevel.Warning))
      .AddDataProtection()
      .PersistKeysToFileSystem(new (keysPath))
      .Services;

}