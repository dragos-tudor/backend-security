using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using static Security.Authentication.Remote.RemoteFuncs;

namespace Security.Sample.Api;
#pragma warning disable CA2000

partial class ApiFuncs
{
  static IServiceCollection AddServices(
    IServiceCollection services,
    ConfigurationManager configuration,
    string encryptionKeysPath,
    string[] origins) =>
      services
        .AddCookies(SetCookieOptions(), cookieBuilder: SetCookieBuilderSameSite(CreateCookieBuilder(), SameSiteMode.None))
        .AddGoogle(SetGoogleOptions(configuration), SetRemoteClient(CreateRemoteClient(), GoogleDefaults.AuthenticationScheme))
        .AddFacebook(SetFacebookOptions(configuration), SetRemoteClient(CreateRemoteClient(), FacebookDefaults.AuthenticationScheme))
        .AddTwitter(SetTwitterOptions(configuration), SetRemoteClient(CreateRemoteClient(), TwitterDefaults.AuthenticationScheme))
        .AddAuthorization()
        .AddLogging()
        .AddCors(o => o.AddDefaultPolicy(BuildCorsPolicy(origins)))
        .AddProblemDetails()
        .AddDataProtection()
        .PersistKeysToFileSystem(new (encryptionKeysPath))
        .Services;
}