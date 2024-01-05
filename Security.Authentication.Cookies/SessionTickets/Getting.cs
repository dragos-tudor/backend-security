
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  const string SessionTicketIdClaim = "Microsoft.AspNetCore.Authentication.Cookies-SessionId";

  static Task<AuthenticationTicket?> GetSessionTicket(ITicketStore ticketStore, string ticketId, CancellationToken token = default) =>
    ticketStore.GetTicket(ticketId, token);

  static Claim GetSessionTicketIdClaim(string ticketId) =>
    new (SessionTicketIdClaim, ticketId);

  static string? GetSessionTicketId(HttpContext context) =>
    GetAuthenticationFeature<SessionTicketId>(context)?.TicketId;

  static string? GetSessionTicketId(ClaimsPrincipal? principal) =>
    principal?.Claims.FirstOrDefault(c => c.Type.Equals(SessionTicketIdClaim))?.Value;
}