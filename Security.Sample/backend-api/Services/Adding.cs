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
        .AddCookiesServices(SetCookieOptions(), cookieBuilder: SetCookieBuilderSameSite(CreateCookieBuilder(), SameSiteMode.None))
        .AddGoogleServices(SetGoogleOptions(configuration), SetRemoteClient(CreateRemoteClient(), GoogleDefaults.AuthenticationScheme))
        .AddFacebookServices(SetFacebookOptions(configuration), SetRemoteClient(CreateRemoteClient(), FacebookDefaults.AuthenticationScheme))
        .AddTwitterServices(SetTwitterOptions(configuration), SetRemoteClient(CreateRemoteClient(), TwitterDefaults.AuthenticationScheme))
        .AddAuthorizationServices()
        .AddLogging()
        .AddCors(o => o.AddDefaultPolicy(BuildCorsPolicy(origins)))
        .AddProblemDetails()
        .AddDataProtection()
        .PersistKeysToFileSystem(new (encryptionKeysPath))
        .Services;
}