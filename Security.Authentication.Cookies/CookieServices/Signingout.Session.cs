
#nullable disable
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async Task<bool> SignOutSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    AuthenticationTicket sessionTicketId,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector,
    ITicketStore ticketStore,
    ILogger logger)
  {
    var sessionId = GetSessionId(sessionTicketId);
    if (sessionId is null) return false;

    var sessionTicket = await GetSessionTicket(ticketStore, sessionId, context.RequestAborted);
    await RemoveSessionTicket(ticketStore, sessionId, context.RequestAborted);

    CleanAuthenticationCookie(context, authOptions, cookieManager);
    ResetHttpResponseCacheHeaders(context.Response);

    LogSignedOutCookie(logger, authOptions.SchemeName, GetPrincipalNameId(sessionTicket.Principal), context.TraceIdentifier);
    return true;
  }
}