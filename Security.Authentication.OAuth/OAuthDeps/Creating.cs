using System.Net.Http;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static OAuthDeps<TOptions> CreateOAuthDeps<TOptions>(
    IServiceProvider services,
    TOptions authOptions,
    HttpClient? httpClient = default,
    IDataProtectionProvider? dataProtectionProvider = default,
    TimeProvider? timeProvider = default)
  where TOptions : OAuthOptions =>
      new(
        httpClient ?? ResolveService<HttpClient>(services),
        CreatePropertiesDataFormat(dataProtectionProvider ?? ResolveService<IDataProtectionProvider>(services), authOptions.SchemeName),
        timeProvider ?? ResolveService<TimeProvider>(services) ?? TimeProvider.System
      );
}