
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static Task<AuthenticateResult> AuthenticateSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    AuthenticationTicket sessionTicketId) =>
      AuthenticateSessionCookie(
        context,
        authOptions,
        sessionTicketId,
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<ICookieManager>(context),
        ResolveRequiredService<TicketDataFormat>(context),
        ResolveRequiredService<ITicketStore>(context)
      );

}