
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static IServiceCollection AddCookies(this IServiceCollection services) =>
    AddCookies(services, CreateCookieAuthenticationOptions());

  public static IServiceCollection AddCookies(
    this IServiceCollection services,
    CookieAuthenticationOptions authOptions,
    CookieBuilder? cookieBuilder = default) =>
      services
        .AddSingleton((services) => authOptions)
        .AddSingleton((services) => cookieBuilder ?? CreateCookieBuilder())
        .AddSingleton<ICookieManager, ChunkingCookieManager>()
        .AddSingleton<ISecureDataFormat<AuthenticationTicket>>((services) =>
          CreateTicketDataFormat(services.GetRequiredService<IDataProtectionProvider>(), authOptions.SchemeName))
        .AddSingleton(CreateCookieBuilder())
        .AddSingleton(TimeProvider.System);

}