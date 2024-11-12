using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket RenewAuthenticationTicket(AuthenticationTicket authTicket, DateTimeOffset currentUtc)
  {
    var expiresAfter = GetAuthenticationPropertiesExpiresAfter(authTicket.Properties);
    SetAuthenticationPropertiesExpiration(authTicket.Properties, currentUtc, expiresAfter);
    return authTicket;
  }
}