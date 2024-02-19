using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Security.Authentication.Remote.RemoteFuncs;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IServiceCollection AddServices(WebApplicationBuilder app, string keysPath) =>
    app.Services
      .AddCookies(SetCookieOptions())
      .AddGoogle(SetGoogleOptions(app), SetRemoteClient(CreateRemoteClient(), GoogleDefaults.AuthenticationScheme))
      .AddFacebook(SetFacebookOptions(app), SetRemoteClient(CreateRemoteClient(), FacebookDefaults.AuthenticationScheme))
      .AddTwitter(SetTwitterOptions(app), SetRemoteClient(CreateRemoteClient(), TwitterDefaults.AuthenticationScheme))
      .AddAuthorizationCore()
      .AddLogging(o => o.SetMinimumLevel(LogLevel.Warning))
      .AddDataProtection()
      .PersistKeysToFileSystem(new (keysPath))
      .Services;
}