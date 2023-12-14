
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static string GetPropertiesRedirectUriOrCurrentUri (
    HttpContext context,
    AuthenticationProperties authProperties) =>
      GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);

  static string? GetPropertiesRedirectUriOrQueryReturnUrl (
    HttpContext context,
    AuthenticationProperties authProperties,
    string returnUrlParameter) =>
      GetAuthenticationPropertiesRedirectUri(authProperties) ?? GetRequestQueryReturnUrl(context.Request, returnUrlParameter);

}