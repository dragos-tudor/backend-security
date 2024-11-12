
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static DateTimeOffset GetAuthenticationTicketExpires(AuthenticationTicket ticket) => ticket.Properties.ExpiresUtc!.Value;

  static DateTimeOffset GetAuthenticationTicketIssued(AuthenticationTicket ticket) => ticket.Properties.IssuedUtc!.Value;

  static IEnumerable<Claim>? GetAuthenticationTicketClaims(AuthenticationTicket ticket) => ticket.Principal?.Claims;

  static TimeSpan GetAuthenticationTicketTimeElapsed(AuthenticationTicket ticket, DateTimeOffset currentUtc) => currentUtc - GetAuthenticationTicketIssued(ticket);

  static TimeSpan GetAuthenticationTicketTimeRemaining(AuthenticationTicket ticket, DateTimeOffset currentUtc) => GetAuthenticationTicketExpires(ticket) - currentUtc;
}