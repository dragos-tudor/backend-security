
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? SetAuthPropsItem(AuthenticationProperties authProps, string key, string? value) { authProps.SetString(key, value); return value; }

  public static T SetAuthPropsParam<T>(AuthenticationProperties authProps, string key, T value) { authProps.SetParameter(key, value); return value; }

  public static DateTimeOffset? SetAuthPropsExpires(AuthenticationProperties authProps, DateTimeOffset? expires) => authProps.ExpiresUtc = expires;

  public static DateTimeOffset? SetAuthPropsExpires(AuthenticationProperties authProps, DateTimeOffset issuedUtc, TimeSpan expiresAfter) => authProps.ExpiresUtc = issuedUtc.Add(expiresAfter);

  public static DateTimeOffset? SetAuthPropsIssued(AuthenticationProperties authProps, DateTimeOffset issued) => authProps.IssuedUtc = issued;

  public static string SetAuthPropsRedirectUri(AuthenticationProperties authProps, string redirectUri) => authProps.RedirectUri ??= redirectUri;
}