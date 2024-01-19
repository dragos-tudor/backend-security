using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectMessage CreateOpenIdConnectMessage(
    HttpContext context,
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration) =>
      new (new Dictionary<string, string[]>()) {
        ClientId = oidcOptions.ClientId,
        EnableTelemetryParameters = !oidcOptions.DisableTelemetry,
        MaxAge = (GetAuthenticationPropertiesMaxAge(authProperties) ?? oidcOptions.MaxAge) switch{
          null => default,
          var maxAge => ToMaxAgeString(maxAge),
        },
        IssuerAddress = oidcConfiguration?.AuthorizationEndpoint ?? string.Empty,
        RedirectUri = BuildAbsoluteUrl(context.Request, oidcOptions.CallbackPath),
        Resource = oidcOptions.Resource,
        ResponseType = oidcOptions.ResponseType,
        Prompt = GetAuthenticationPropertiesPrompt(authProperties) ?? oidcOptions.Prompt,
        Scope = string.Join(" ", GetAuthenticationPropertiesScope(authProperties) ?? oidcOptions.Scope),
      };
}