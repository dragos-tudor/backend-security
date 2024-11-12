
#nullable disable
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<bool> SignOutSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    AuthenticationTicket sessionIdTicket,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataProtector,
    ITicketStore ticketStore,
    ILogger logger)
  {
    await CleanSessionTicket(context, sessionIdTicket, GetSessionId(sessionIdTicket), authOptions, cookieManager, ticketStore);
    ResetResponseCacheHeaders(context.Response);

    LogSignedOutCookie(logger, authOptions.SchemeName, context.TraceIdentifier);
    return true;
  }
}