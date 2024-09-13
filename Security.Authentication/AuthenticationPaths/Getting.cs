
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetRedirectUriOrQueryReturnUrl (HttpContext context, AuthenticationProperties authProperties, AuthenticationOptions authOptions) =>
    GetAuthenticationPropertiesRedirectUri(authProperties) ??
    GetRequestQueryValue(context.Request, authOptions.ReturnUrlParameter);
}