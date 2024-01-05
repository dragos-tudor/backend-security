using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket CreateSessionIdTicket(
    string ticketId,
    string schemeName) =>
      CreateAuthenticationTicket(
        CreatePrincipal(schemeName, [GetSessionTicketIdClaim(ticketId)]),
        null,
        schemeName);

}