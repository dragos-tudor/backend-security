
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetCodeVerifier(OAuthOptions oauthOptions, AuthenticationProperties authProps) => ShouldUseCodeChallenge(oauthOptions) ? GetAuthPropsCodeVerifier(authProps) : default;
}