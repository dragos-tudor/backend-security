using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string? ExtractSessionTicketId(
    HttpContext context,
    ICookieManager cookieManager,
    string cookieName,
    ISecureDataFormat<AuthenticationTicket> ticketProtector)
  {
    if(GetAuthenticationCookie(context, cookieManager, cookieName) is not string cookie) return default;
    if(UnprotectAuthenticationTicket(cookie, ticketProtector) is not AuthenticationTicket cookieTicket) return default;
    return GetSessionTicketId(cookieTicket.Principal);
  }

}