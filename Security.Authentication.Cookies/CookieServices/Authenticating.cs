
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;
#nullable disable

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string TicketExpired = "ticket expired";
  internal const string UnprotectTicketFailed = "unprotect ticket failed";

  public static async ValueTask<AuthenticateResult> AuthenticateCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector,
    ITicketStore ticketStore)
  {
    if (IsSignInHttpRequest(context.Request, authOptions)) return NoResult();

    var (authTicket, error) = ExtractAuthenticationCookieTicket(context, authOptions, cookieManager, authTicketProtector);
    if (error == NoCookie) return NoResult();
    if (error is not null) return Fail(error);

    if (IsSessionBasedTicket(ticketStore))
      return await AuthenticateSessionCookie(context, authOptions, authTicket, currentUtc, cookieManager, authTicketProtector, ticketStore);

    var authTicketState = GetAuthenticationTicketState(authTicket, currentUtc, authOptions);
    if (authTicketState == AuthenticationTicketState.Valid) return Success(authTicket);
    if (authTicketState == AuthenticationTicketState.Expired) {
      CleanAuthenticationCookie(context, authOptions, cookieManager);
      return Fail(TicketExpired);
    }

    var renewedAuthTicket = RenewAuthenticationTicket(authTicket, authOptions, currentUtc);
    SetAuthenticationCookie(context, renewedAuthTicket, authOptions, currentUtc, cookieManager, authTicketProtector);
    ResetHttpResponseCacheHeaders(context.Response);

    return Success(renewedAuthTicket);
  }
}