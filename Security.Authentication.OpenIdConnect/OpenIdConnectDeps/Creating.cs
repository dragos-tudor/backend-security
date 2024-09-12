using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectDeps<TOptions> CreateOpenIdConnectDeps<TOptions>(
    IServiceProvider services,
    TOptions authOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OpenIdConnectOptions =>
      new(
        httpClient ?? ResolveRequiredService<HttpClient>(services),
        CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), authOptions.SchemeName),
        CreateStringDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), authOptions.SchemeName),
        timeProvider ?? ResolveRequiredService<TimeProvider>(services) ?? TimeProvider.System
      );
}