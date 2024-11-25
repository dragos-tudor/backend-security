
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string NoCookie = "no cookie";
  internal const string UnprotectingCookieFailed = "unprotecting cookie failed";

  static(AuthenticationTicket? authTicket, string? error) ExtractAuthenticationCookieTicket(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector)
  {
    var authCookieName = GetAuthenticationCookieName(authOptions);
    var authCookie = GetCookie(context, cookieManager, authCookieName);
    if (authCookie is null) return(default, NoCookie);

    var authTicket = authTicketProtector.Unprotect(authCookie);
    if (authTicket is null) return(default, UnprotectingCookieFailed);

    return(authTicket, default);
  }
}