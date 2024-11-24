using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static Task<string?> GetOidcParamIdTokenHint(HttpContext context, OpenIdConnectOptions oidcOptions) => context.GetTokenAsync(oidcOptions.SchemeName, OidcParamNames.IdToken);

  static string? GetOidcParamMaxAge(AuthenticationProperties authProps, OpenIdConnectOptions oidcOptions) => (GetAuthPropsMaxAge(authProps) ?? oidcOptions.MaxAge)?.ToOidcParamMaxAgeString();

  static string GetOidcParamPrompt(AuthenticationProperties authProps, OpenIdConnectOptions oidcOptions) => GetAuthPropsPrompt(authProps) ?? oidcOptions.Prompt;

  static string GetOidcParamScope(AuthenticationProperties authProps, OpenIdConnectOptions oidcOptions) => string.Join(" ", GetAuthPropsScope(authProps) ?? oidcOptions.Scope);
}