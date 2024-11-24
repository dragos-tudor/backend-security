
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal static AuthenticationTicket CreateAuthenticationTicket(ClaimsPrincipal principal, AuthenticationProperties? authProps, string schemeName) => new(principal, authProps, schemeName);
}