
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string? GetRedirectUriOrQueryReturnUrl (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions) =>
      GetAuthenticationPropertiesRedirectUri(authProperties) ??
      GetRequestQueryValue(context.Request, authOptions.ReturnUrlParameter);
}