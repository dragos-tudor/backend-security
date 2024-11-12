
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket CleanAuthenticationTicket(
    HttpContext context,
    AuthenticationTicket authTicket,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager)
  {
    var authCookieName = GetAuthenticationCookieName(authOptions);
    var cookieOptions = BuildCookieOptions(authTicket.Properties!, context);
    DeleteCookie(context, cookieManager, authCookieName, cookieOptions);

    return authTicket;
  }
}