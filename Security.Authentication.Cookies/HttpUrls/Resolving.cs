
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string? ResolveRedirectUri (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions) =>
      GetAuthenticationPropertiesRedirectUri(authProperties) ??
      GetRequestQueryReturnUrl(context.Request, authOptions.ReturnUrlParameter);
}