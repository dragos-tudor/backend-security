
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static AuthenticationTicket CreateAuthenticationTicket (ClaimsPrincipal principal, AuthenticationProperties? authProperties, string schemeName) =>
    new (principal, authProperties, schemeName);

}