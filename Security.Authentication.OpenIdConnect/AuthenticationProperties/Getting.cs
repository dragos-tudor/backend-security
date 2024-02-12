using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TimeSpan? GetAuthenticationPropertiesMaxAge(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesParam<TimeSpan>(authProperties, OpenIdConnectParameterNames.MaxAge);

  static string? GetAuthenticationPropertiesPrompt(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesParam<string>(authProperties, OpenIdConnectParameterNames.Prompt);

  static string? GetAuthenticationPropertiesRedirectUriForCode(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, OpenIdConnectDefaults.RedirectUriForCodePropertiesKey);

  static ICollection<string>? GetAuthenticationPropertiesScope(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesParam<ICollection<string>>(authProperties, OpenIdConnectParameterNames.Scope);

  static string? GetAuthenticationPropertiesUserState(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesItem(authProperties, OpenIdConnectDefaults.UserStatePropertiesKey);
}