
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static AuthenticationTicket CreateAuthenticationTicket (ClaimsPrincipal principal, AuthenticationProperties? authProperties, string schemeName) =>
    new (principal, authProperties, schemeName);

}