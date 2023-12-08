
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static void SetResponseCookieHeader (
    HttpContext context,
    AuthenticationTicket ticket,
    CookieAuthenticationOptions authOptions,
    CookieOptions cookieOptions,
    string cookieName) =>
      AppendAuthenticationCookie(
        context,
        authOptions.CookieManager,
        cookieName,
        ProtectAuthenticationTicket(ticket, authOptions.TicketDataFormat),
        cookieOptions
      );

}