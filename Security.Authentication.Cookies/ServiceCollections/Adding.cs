
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Cookies;

partial class Funcs {

  public static IServiceCollection AddCookies(
    this IServiceCollection services,
    ConfigFunc<CookieAuthenticationOptions>? configOptionsFunc = default,
    ConfigFunc<CookieBuilder>? configBuilderFunc = default,
    string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
      services
        .AddSingleton((services) => (configOptionsFunc ?? Identity)(
          CreateCookieAuthenticationOptions(ResolveService<ICookieManager>(services), ResolveService<IDataProtectionProvider>(services), schemeName)))
        .AddSingleton((services) => (configBuilderFunc ?? Identity)(CreateCookieBuilder()))
        .TryAddSingleton<ICookieManager, ChunkingCookieManager>()
        .TryAddSingleton(TimeProvider.System);

}