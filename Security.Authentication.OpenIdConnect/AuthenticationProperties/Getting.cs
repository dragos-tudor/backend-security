
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TimeSpan? GetAuthPropsMaxAge(AuthenticationProperties authProps) => GetAuthPropsParam<TimeSpan>(authProps, OidcParamNames.MaxAge);

  static string? GetAuthPropsPrompt(AuthenticationProperties authProps) => GetAuthPropsParam<string>(authProps, OidcParamNames.Prompt);

  static string? GetAuthPropsUserState(AuthenticationProperties authProps) => GetAuthPropsItem(authProps, OidcDefaults.UserStateProperties);

  static ICollection<string>? GetAuthPropsScope(AuthenticationProperties authProps) => GetAuthPropsParam<ICollection<string>>(authProps, OidcParamNames.Scope);
}