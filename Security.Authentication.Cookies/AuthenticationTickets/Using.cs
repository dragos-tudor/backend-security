
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket UseAuthenticationTicket(
    HttpContext context,
    AuthenticationTicket authTicket,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataProtector)
  {
    var authCookieName = GetAuthenticationCookieName(authOptions);
    var cookieOptions = BuildCookieOptions(authTicket.Properties!, context);
    var protectedAuthTicket = ProtectAuthenticationTicket(authTicket, ticketDataProtector);

    AppendCookie(context, cookieManager, authCookieName, protectedAuthTicket, cookieOptions);
    return authTicket;
  }
}