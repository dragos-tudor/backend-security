
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal static string GetChallengePath(AuthenticationOptions authOptions, string returnUri) => $"{authOptions.LoginPath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";

  internal static string GetForbidPath(AuthenticationOptions authOptions, string returnUri) => $"{authOptions.AccessDeniedPath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";

  internal static string? GetRedirectUriOrQueryReturnUrl(HttpContext context, AuthenticationProperties authProperties, AuthenticationOptions authOptions) =>
    GetAuthenticationPropertiesRedirectUri(authProperties) ?? GetRequestQueryValue(context.Request, authOptions.ReturnUrlParameter);
}