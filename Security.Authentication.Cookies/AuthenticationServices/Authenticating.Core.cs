
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string TicketExpired = "Ticket expired";
  const string UnprotectTicketFailed = "Unprotect ticket failed";
  const string MissingSessionTicketId = "Missing session ticket id";
  const string MissingSessionTicket = "Missing session ticket";

  static AuthenticateResult AuthenticateCoreResult (
    AuthenticationTicket ticket,
    CookieAuthenticationOptions authOptions,
    DateTimeOffset currentUtc) =>
      GetAuthenticationTicketState(ticket, currentUtc, authOptions) switch
      {
        AuthenticationTicketState.ExpiredTicket => Fail(TicketExpired),
        AuthenticationTicketState.RenewableTicket => Success(RenewAuthenticationTicket(ticket, currentUtc)),
        _ => Success(ticket)
      };

  internal static async ValueTask<AuthenticateResult> AuthenticateCoreCookie (
    HttpContext context,
    CookieAuthenticationOptions authOptions,
    CookieBuilder cookieBuilder,
    ICookieManager cookieManager,
    ISecureDataFormat<AuthenticationTicket> ticketProtector,
    ITicketStore ticketStore,
    DateTimeOffset currentUtc)
  {
    var cookieName = GetCookieName(cookieBuilder, authOptions);
    var cookie = GetAuthenticationCookie(context, cookieManager, cookieName);
    if (cookie is null) return NoResult();

    var cookieTicket = UnprotectAuthenticationTicket(cookie, ticketProtector);
    if (cookieTicket is null) return Fail(UnprotectTicketFailed);

    if (!ExistsTicketStore(ticketStore))
      return AuthenticateCoreResult(cookieTicket, authOptions, currentUtc);

    var ticketId = GetSessionTicketId(cookieTicket.Principal);
    if (ticketId is null) return Fail(MissingSessionTicketId);

    var sessionTicket = await GetSessionTicket(ticketStore, ticketId, context.RequestAborted);
    if (sessionTicket is null) return Fail(MissingSessionTicket);

    SetSessionTicketId(context, ticketId);
    return AuthenticateCoreResult(sessionTicket, authOptions, currentUtc);
  }

}