
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static AuthenticationTicket CreateAuthenticationTicket (ClaimsPrincipal principal, AuthenticationProperties? authProperties, string schemeName) =>
    new (principal, authProperties, schemeName);

}