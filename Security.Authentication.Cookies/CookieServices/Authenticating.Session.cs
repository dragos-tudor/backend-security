
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string MissingSessionId = "missing session id";
  internal const string MissingSessionTicket = "missing session ticket";

  public static async Task<AuthenticateResult> AuthenticateSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    AuthenticationTicket sessionTicketId,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat authTicketProtector,
    ITicketStore ticketStore)
  {
    var sessionId = GetSessionId(sessionTicketId);
    if (sessionId is null) return Fail(MissingSessionId);

    var sessionTicket = await GetSessionTicket(ticketStore, sessionId, context.RequestAborted);
    if (sessionTicket is null) return Fail(MissingSessionTicket);

    var sessionTicketState = GetAuthenticationTicketState(sessionTicket, currentUtc, authOptions);
    if (sessionTicketState == AuthenticationTicketState.Valid) return Success(sessionTicket);
    if (sessionTicketState == AuthenticationTicketState.Expired) {
      await RemoveSessionTicket(ticketStore, sessionId, context.RequestAborted);
      CleanAuthenticationCookie(context, authOptions, cookieManager);
      return Fail(TicketExpired);
    }

    var renewedSessionTicketId = RenewAuthenticationTicket(sessionTicketId, authOptions, currentUtc);
    SetAuthenticationCookie(context, renewedSessionTicketId, authOptions, currentUtc, cookieManager, authTicketProtector);

    var renewedSessionTicket = RenewAuthenticationTicket(sessionTicket, authOptions, currentUtc);
    await RenewSessionTicket(ticketStore, renewedSessionTicket, sessionId, context.RequestAborted);
    ResetHttpResponseCacheHeaders(context.Response);

    return Success(renewedSessionTicket);
  }
}