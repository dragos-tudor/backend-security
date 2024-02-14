
using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static IServiceCollection AddOpenIdConnect<TOptions>(
    this IServiceCollection services,
    TOptions authOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OpenIdConnectOptions =>
      services
        .AddSingleton(authOptions)
        .AddSingleton(services =>
          CreateOpenIdConnectDeps(services, authOptions, httpClient, dataProtectionProvider, timeProvider));
}