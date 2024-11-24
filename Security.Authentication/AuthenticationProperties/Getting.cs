
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetAuthPropsItem(AuthenticationProperties authProps, string key) => authProps.GetString(key);

  public static T? GetAuthPropsParam<T>(AuthenticationProperties authProps, string key) => authProps.GetParameter<T>(key);

  public static string? GetAuthPropsRedirectUri(AuthenticationProperties authProps) => authProps.RedirectUri;
}