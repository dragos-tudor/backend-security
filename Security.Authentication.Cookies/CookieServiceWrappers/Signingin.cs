
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static ValueTask<AuthenticationTicket> SignInCookie(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProps = default) =>
      SignInCookie(
        context,
        principal,
        authProps ?? CreateAuthProps(),
        ResolveRequiredService<AuthenticationCookieOptions>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context),
        ResolveCookiesLogger(context));
}