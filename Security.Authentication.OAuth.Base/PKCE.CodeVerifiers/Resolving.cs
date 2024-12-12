
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? ResolveCodeVerifier(OAuthOptions oauthOptions, AuthenticationProperties authProps) => ShouldUseCodeChallenge(oauthOptions) ? GetAuthPropsCodeVerifier(authProps) : default;

  public static string? ResolveCodeVerifier(OAuthOptions oauthOptions) => ShouldUseCodeChallenge(oauthOptions) ? GenerateCodeVerifier() : default;
}