
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static IServiceCollection AddOAuth<TOptions>(
    this IServiceCollection services,
    TOptions authOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OAuthOptions =>
      services
        .AddSingleton(authOptions)
        .AddSingleton(services =>
          CreateOAuthDeps(services, authOptions, httpClient, dataProtectionProvider, timeProvider));
}