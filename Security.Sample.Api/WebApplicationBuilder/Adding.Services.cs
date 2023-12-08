
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Security.Samples;

partial class Funcs {

  internal static IServiceCollection AddServices(WebApplicationBuilder app) =>
    app.Services
      .AddCookies(ConfigureCookieOptions)
      .AddGoogle(ConfigureGoogleOptions(app))
      .AddFacebook(ConfigureFacebookOptions(app))
      .AddTwitter(ConfigureTwitterOptions(app))
      .AddDataProtection()
      .PersistKeysToFileSystem(new (Environment.CurrentDirectory + "/bin/keys")).Services
      .AddLogging(o => o.SetMinimumLevel(LogLevel.Warning));

}