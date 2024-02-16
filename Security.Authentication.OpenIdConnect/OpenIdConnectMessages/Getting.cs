using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static Task<string?> GetOpenIdConnectMessageIdTokenHint(HttpContext context, OpenIdConnectOptions oidcOptions) =>
    context.GetTokenAsync(oidcOptions.SignOutScheme, OpenIdConnectParameterNames.IdToken);

  static string? GetOpenIdConnectMessageMaxAge(AuthenticationProperties authProperties, OpenIdConnectOptions oidcOptions) =>
    (GetAuthenticationPropertiesMaxAge(authProperties) ?? oidcOptions.MaxAge)?.ToOpenIdConnectMessageMaxAgeString();

  static string? GetOpenIdConnectMessagePrompt(AuthenticationProperties authProperties, OpenIdConnectOptions oidcOptions) =>
    GetAuthenticationPropertiesPrompt(authProperties) ?? oidcOptions.Prompt;

  static string GetOpenIdConnectMessageRedirectUri(HttpContext context, OpenIdConnectOptions oidcOptions) =>
    BuildAbsoluteUrl(context.Request, oidcOptions.CallbackPath);

  static string GetOpenIdConnectMessageScope(AuthenticationProperties authProperties, OpenIdConnectOptions oidcOptions) =>
    string.Join(" ", GetAuthenticationPropertiesScope(authProperties) ?? oidcOptions.Scope);
}