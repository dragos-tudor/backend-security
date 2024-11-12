
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal const string MissingSessionId = "Missing session id";
  internal const string MissingSessionTicket = "Missing session ticket";

  public static async Task<AuthenticateResult> AuthenticateSessionCookie(
    HttpContext context,
    AuthenticationCookieOptions authOptions,
    AuthenticationTicket sessionTicketId,
    DateTimeOffset currentUtc,
    ICookieManager cookieManager,
    TicketDataFormat ticketDataProtector,
    ITicketStore ticketStore)
  {
    var sessionId = GetSessionId(sessionTicketId);
    if(sessionId is null) return Fail(MissingSessionId);

    var sessionTicket = await GetSessionTicket(ticketStore, sessionId, context.RequestAborted);
    if(sessionTicket is null) return Fail(MissingSessionTicket);

    var sessionTicketState = GetAuthenticationTicketState(sessionTicket, currentUtc, authOptions);
    if(sessionTicketState == AuthenticationTicketState.Valid) return Success(sessionTicket);
    if(sessionTicketState == AuthenticationTicketState.Expired) {
      await CleanSessionTicket(context, sessionTicket, sessionId, authOptions, cookieManager, ticketStore);
      return Fail(TicketExpired);
    }

    var renewedSessionTicketId = RenewAuthenticationTicket(sessionTicketId, currentUtc);
    UseAuthenticationTicket(context, renewedSessionTicketId, authOptions, cookieManager, ticketDataProtector);

    var renewedSessionTicket = RenewAuthenticationTicket(sessionTicket, currentUtc);
    await RenewSessionTicket(ticketStore, renewedSessionTicket, sessionId, context.RequestAborted);
    ResetResponseCacheHeaders(context.Response);

    return Success(renewedSessionTicket);
  }
}