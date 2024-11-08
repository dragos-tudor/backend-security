
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static IServiceCollection AddCookiesServices (this IServiceCollection services) =>
      AddCookiesServices(services, CreateAuthenticationCookieOptions());

  public static IServiceCollection AddCookiesServices (
    this IServiceCollection services,
    AuthenticationCookieOptions authOptions,
    ITicketStore? ticketStore = default,
    CookieBuilder? cookieBuilder = default,
    IDataProtectionProvider? dataProtectionProvider = default) =>
      services
        .AddSingleton((services) => authOptions)
        .AddSingleton<ICookieManager, ChunkingCookieManager>()
        .AddSingleton((services) =>
          CreateTicketDataFormat(dataProtectionProvider ?? ResolveRequiredService<IDataProtectionProvider>(services), authOptions.SchemeName))
        .AddSingleton(ticketStore ?? new DefaultTicketStore())
        .AddSingleton(TimeProvider.System)
        .AddKeyedSingleton(CookiesLogger, (services, serviceKey) => CreateLogger(services, (string)serviceKey));
}