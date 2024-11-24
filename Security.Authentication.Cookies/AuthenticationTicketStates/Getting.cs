using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicketState GetAuthenticationTicketState(
    AuthenticationTicket ticket,
    DateTimeOffset currentUtc,
    AuthenticationCookieOptions authOptions)
  {
    if(IsExpiredAuthenticationTicket(ticket, currentUtc)) return AuthenticationTicketState.Expired;
    if(IsRenewableAuthenticationTicket(ticket, currentUtc)) return AuthenticationTicketState.Renewable;
    return AuthenticationTicketState.Valid;
  }
}