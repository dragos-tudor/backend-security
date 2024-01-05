using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket RenewAuthenticationTicket(AuthenticationTicket ticket, DateTimeOffset currentUtc)
  {
    var expiresAfter = GetAuthenticationPropertiesExpiresAfter(ticket.Properties);
    SetAuthenticationPropertiesIssued(ticket.Properties, currentUtc);
    SetAuthenticationPropertiesExpires(ticket.Properties, currentUtc, expiresAfter);
    return ticket;
  }
}