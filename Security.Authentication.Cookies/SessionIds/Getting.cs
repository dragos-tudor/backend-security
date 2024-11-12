
using System.Linq;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string SessionIdClaim = "Microsoft.AspNetCore.Authentication.Cookies-SessionId";

  static Claim GetSessionIdClaim(string ticketId) => new(SessionIdClaim, ticketId);

  internal static string? GetSessionId(AuthenticationTicket authTicket) => GetAuthenticationTicketClaims(authTicket)?.FirstOrDefault(IsSessionIdClaim)?.Value;
}