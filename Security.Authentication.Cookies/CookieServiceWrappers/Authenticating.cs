
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async Task<AuthenticateResult> AuthenticateCookie(HttpContext context) =>
    LogAuthentication(
      ResolveCookiesLogger(context),
      await AuthenticateCookie(
        context,
        ResolveRequiredService<AuthenticationCookieOptions>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context)
      ),
      ResolveRequiredService<AuthenticationCookieOptions>(context).SchemeName,
      context.TraceIdentifier
    );
}