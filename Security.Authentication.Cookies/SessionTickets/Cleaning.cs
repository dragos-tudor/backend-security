
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static async Task<AuthenticationTicket> CleanSessionTicket(
    HttpContext context,
    AuthenticationTicket sessionTicketId,
    string sessionId,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    ITicketStore ticketStore)
  {
    await RemoveSessionTicket(ticketStore, sessionId, context.RequestAborted);
    CleanAuthenticationTicket(context, sessionTicketId, authOptions, cookieManager);
    return sessionTicketId;
  }
}