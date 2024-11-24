
#nullable disable
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static async ValueTask<bool> SignOutCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector,
    ITicketStore ticketStore,
    ILogger logger)
  {
    var(authTicket, error) = ExtractAuthenticationCookieTicket(context, authOptions, cookieManager, authTicketProtector);
    if(error is not null) return false;

    if(IsSessionBasedTicket(ticketStore))
      return await SignOutSessionCookie(context, authOptions, authTicket, cookieManager, authTicketProtector, ticketStore, logger);

    CleanAuthenticationCookie(context, authOptions, cookieManager);
    ResetHttpResponseCacheHeaders(context.Response);

    LogSignedOutCookie(logger, authOptions.SchemeName, GetPrincipalNameId(authTicket.Principal)!, context.TraceIdentifier);
    return true;
  }
}