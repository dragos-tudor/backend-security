
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
    TicketDataFormat ticketDataProtector,
    ITicketStore ticketStore,
    ILogger logger)
  {
    var(authTicket, error) = ExtractAuthenticationTicket(context, authOptions, cookieManager, ticketDataProtector);
    if(error is not null) return false;

    if(IsSessionBasedTicket(ticketStore))
      return await SignOutSessionCookie(context, authOptions, authTicket, cookieManager, ticketDataProtector, ticketStore, logger);

    CleanAuthenticationTicket(context, authTicket, authOptions, cookieManager);
    ResetResponseCacheHeaders(context.Response);

    LogSignedOutCookie(logger, authOptions.SchemeName, context.TraceIdentifier);
    return true;
  }
}