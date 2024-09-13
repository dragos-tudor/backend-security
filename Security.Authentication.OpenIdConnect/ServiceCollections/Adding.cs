
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static IServiceCollection AddOpenIdConnectServices<TOptions>(
    this IServiceCollection services,
    TOptions authOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OpenIdConnectOptions =>
      services
        .AddSingleton(authOptions)
        .AddSingleton(oidcConfiguration)
        .AddSingleton(httpClient)
        .AddSingleton(services => CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), authOptions.SchemeName))
        .AddSingleton(services => CreateStringDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), authOptions.SchemeName))
        .AddSingleton(timeProvider);
}