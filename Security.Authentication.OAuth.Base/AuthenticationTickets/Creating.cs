
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static AuthenticationTicket CreateAuthenticationTicket(ClaimsPrincipal principal, AuthenticationProperties? authProps, string schemeName) => new(principal, authProps, schemeName);
}