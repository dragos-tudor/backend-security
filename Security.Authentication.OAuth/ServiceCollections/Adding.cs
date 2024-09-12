
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static IServiceCollection AddOAuthServices<TOptions>(
    this IServiceCollection services,
    TOptions authOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OAuthOptions =>
      services
        .AddSingleton(authOptions)
        .AddSingleton(services => CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), authOptions.SchemeName))
        .AddSingleton(httpClient)
        .AddSingleton(timeProvider)
        .AddKeyedSingleton(CategoryNameLogger, (services, serviceKey) => CreateLogger(services, (string)serviceKey));
}
