
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string? GetSigningRedirectUri (
    HttpContext context,
    AuthenticationProperties authProperties,
    string returnUrlParameter) =>
      GetAuthenticationPropertiesRedirectUri(authProperties) ??
      GetRequestQueryReturnUrl(context.Request, returnUrlParameter);
}