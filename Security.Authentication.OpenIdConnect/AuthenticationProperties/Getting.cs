using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static T? GetAuthenticationPropertiesParam<T>(AuthenticationProperties authProperties, string paramName) =>
    authProperties.GetParameter<T>(paramName);

  static TimeSpan? GetAuthenticationPropertiesMaxAge(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesParam<TimeSpan>(authProperties, OpenIdConnectParameterNames.MaxAge);

  static string? GetAuthenticationPropertiesPrompt(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesParam<string>(authProperties, OpenIdConnectParameterNames.Prompt);

  static ICollection<string>? GetAuthenticationPropertiesScope(AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesParam<ICollection<string>>(authProperties, OpenIdConnectParameterNames.Scope);

  static string? GetAuthenticationPropertiesUserState(AuthenticationProperties authProperties) =>
    authProperties.Items.TryGetValue(OpenIdConnectDefaults.UserStatePropertiesKey, out var userState)?
      userState: default;
}