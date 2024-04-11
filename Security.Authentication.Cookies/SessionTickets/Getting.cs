
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  const string SessionTicketIdClaim = "Microsoft.AspNetCore.Authentication.Cookies-SessionId";

  static Task<AuthenticationTicket?> GetSessionTicket(ITicketStore ticketStore, string ticketId, CancellationToken cancellationToken = default) =>
    ticketStore.GetTicket(ticketId, cancellationToken);

  static Claim GetSessionTicketIdClaim(string ticketId) =>
    new (SessionTicketIdClaim, ticketId);

  internal static string? GetSessionTicketId(ClaimsPrincipal? principal) =>
    principal?.Claims.FirstOrDefault(c => c.Type.Equals(SessionTicketIdClaim, StringComparison.Ordinal))?.Value;
}