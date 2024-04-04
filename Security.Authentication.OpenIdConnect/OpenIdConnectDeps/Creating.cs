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
        httpClient ?? ResolveService<HttpClient>(services),
        CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveService<IDataProtectionProvider>(services), authOptions.SchemeName),
        CreateStringDataFormat(dataProtectionProvider ?? ResolveService<IDataProtectionProvider>(services), authOptions.SchemeName),
        timeProvider ?? ResolveService<TimeProvider>(services) ?? TimeProvider.System
      );
}