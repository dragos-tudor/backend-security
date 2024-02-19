using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using static Security.Authentication.Remote.RemoteFuncs;

namespace Security.Sample.Api;

partial class SampleFuncs
{
  static IServiceCollection AddServices(WebApplicationBuilder builder, string keysPath) =>
    builder.Services
      .AddCookies(SetCookieOptions())
      .AddGoogle(SetGoogleOptions(builder), SetRemoteClient(CreateRemoteClient(), GoogleDefaults.AuthenticationScheme))
      .AddFacebook(SetFacebookOptions(builder), SetRemoteClient(CreateRemoteClient(), FacebookDefaults.AuthenticationScheme))
      .AddTwitter(SetTwitterOptions(builder), SetRemoteClient(CreateRemoteClient(), TwitterDefaults.AuthenticationScheme))
      .AddAuthorizationCore()
      .AddLogging(o => o.SetMinimumLevel(LogLevel.Warning))
      .AddDataProtection()
      .PersistKeysToFileSystem(new (keysPath))
      .Services;
}