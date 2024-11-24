using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TimeSpan? GetAuthPropsMaxAge(AuthenticationProperties authProps) => GetAuthPropsParam<TimeSpan>(authProps, OidcParamNames.MaxAge);

  static string? GetAuthPropsPrompt(AuthenticationProperties authProps) => GetAuthPropsParam<string>(authProps, OidcParamNames.Prompt);

  static ICollection<string>? GetAuthPropsScope(AuthenticationProperties authProps) => GetAuthPropsParam<ICollection<string>>(authProps, OidcParamNames.Scope);


  static string? GetAuthPropsRedirectUriForCode(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, OidcDefaults.RedirectUriForCodeProperties);

  static string? GetAuthPropsUserState(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, OidcDefaults.UserStateProperties);
}