
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static IServiceCollection AddCookiesServices(
    this IServiceCollection services,
    AuthenticationCookieOptions? authOptions = default,
    ITicketStore? ticketStore = default,
    ICookieManager? cookieManager = default) =>
      services
        .AddSingleton(authOptions ?? CreateAuthenticationCookieOptions())
        .AddSingleton(cookieManager ?? new ChunkingCookieManager())
        .AddSingleton(ticketStore ?? new DefaultTicketStore())
        .AddSingleton((services) => CreateTicketDataFormat(ResolveRequiredService<IDataProtectionProvider>(services), ResolveRequiredService<AuthenticationCookieOptions>(services).SchemeName))
        .AddKeyedSingleton(CookiesLogger, (services, serviceKey) => CreateLogger(services, (string)serviceKey))
        .TryAddSingleton(TimeProvider.System);
}