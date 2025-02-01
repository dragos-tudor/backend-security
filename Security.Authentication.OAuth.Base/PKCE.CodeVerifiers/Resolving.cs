
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? ResolveCodeVerifier(OAuthOptions oauthOptions) => ShouldUseCodeChallenge(oauthOptions) ? GenerateCodeVerifier() : default;
}