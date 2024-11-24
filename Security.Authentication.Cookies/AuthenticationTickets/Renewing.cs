using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static AuthenticationTicket RenewAuthenticationTicket(
    AuthenticationTicket authTicket,
    AuthenticationCookieOptions authOptions,
    DateTimeOffset currentUtc)
  {
    SetAuthPropsExpiration(authTicket.Properties, currentUtc, authOptions.ExpireAfter);
    return authTicket;
  }
}