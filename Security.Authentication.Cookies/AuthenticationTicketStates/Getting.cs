using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicketState GetAuthenticationTicketState (
    AuthenticationTicket ticket,
    DateTimeOffset currentUtc,
    CookieAuthenticationOptions authOptions)
  {
    if (IsExpiredAuthenticationTicket(ticket, currentUtc)) return AuthenticationTicketState.ExpiredTicket;
    if (IsRenewableAuthenticationTicket(ticket, currentUtc, authOptions.SlidingExpiration)) return AuthenticationTicketState.RenewableTicket;
    return AuthenticationTicketState.Regular;
  }
}