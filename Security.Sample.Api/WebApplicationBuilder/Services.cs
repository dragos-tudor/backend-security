using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using static Security.Authentication.Remote.RemoteFuncs;

namespace Security.Sample.Api;
#pragma warning disable CA2000

partial class SampleFuncs
{
  static IServiceCollection AddServices(
    WebApplicationBuilder builder,
    string keysPath,
    string[] origins) =>
      builder.Services
        .AddCookies(SetCookieOptions(), cookieBuilder: SetCookieBuilderSameSite(CreateCookieBuilder(), SameSiteMode.None))
        .AddGoogle(SetGoogleOptions(builder), SetRemoteClient(CreateRemoteClient(), GoogleDefaults.AuthenticationScheme))
        .AddFacebook(SetFacebookOptions(builder), SetRemoteClient(CreateRemoteClient(), FacebookDefaults.AuthenticationScheme))
        .AddTwitter(SetTwitterOptions(builder), SetRemoteClient(CreateRemoteClient(), TwitterDefaults.AuthenticationScheme))
        .AddAuthorization()
        .AddLogging()
        .AddCors(o => o.AddDefaultPolicy(BuildCorsPolicy(origins)))
        .AddProblemDetails()
        .AddDataProtection()
        .PersistKeysToFileSystem(new (keysPath))
        .Services;
}