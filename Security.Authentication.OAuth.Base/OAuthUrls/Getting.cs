
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public const string HomeUri = "/";

  public static string GetOAuthRedirectUri (AuthenticationProperties authProps) => GetAuthPropsRedirectUri(authProps) ?? HomeUri;
}