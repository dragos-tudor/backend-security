
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket SetAuthenticationCookie(
    HttpContext context,
    AuthenticationTicket authTicket,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector)
  {
    var cookieName = GetAuthenticationCookieName(authOptions);
    var cookieOptions = BuildCookieOptions(context, authOptions, currentUtc);
    var protectedAuthTicket = ProtectAuthenticationTicket(authTicket, authTicketProtector);

    AppendCookie(context, cookieManager, cookieName, protectedAuthTicket, cookieOptions);
    return authTicket;
  }
}