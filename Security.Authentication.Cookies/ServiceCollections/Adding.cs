
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Cookies;

partial class Funcs {

  public static IServiceCollection AddCookies(
    this IServiceCollection services,
    SetFunc<CookieAuthenticationOptions>? setOptions = default,
    SetFunc<CookieBuilder>? setBuilder = default,
    string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
      services
        .AddSingleton((services) => (setOptions ?? Identity)(
          CreateCookieAuthenticationOptions(ResolveService<ICookieManager>(services), ResolveService<IDataProtectionProvider>(services), schemeName)))
        .AddSingleton((services) => (setBuilder ?? Identity)(CreateCookieBuilder()))
        .TryAddSingleton<ICookieManager, ChunkingCookieManager>()
        .TryAddSingleton(TimeProvider.System);

}