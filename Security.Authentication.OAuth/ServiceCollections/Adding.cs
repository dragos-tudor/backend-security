
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static IServiceCollection AddOAuthServices<TOptions>(
    this IServiceCollection services,
    TOptions oauthOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OAuthOptions =>
      services
        .AddSingleton(oauthOptions)
        .AddSingleton(services => CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), oauthOptions.SchemeName))
        .AddSingleton(httpClient)
        .AddSingleton(timeProvider);
}
