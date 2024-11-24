using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal static AuthenticationTicket CreateSessionTicketId(string sessionId, string schemeName) =>
    CreateAuthenticationTicket(CreatePrincipal(schemeName, [GetSessionIdClaim(sessionId)]), CreateAuthProps(), schemeName);
}