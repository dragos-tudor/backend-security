namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ShouldCleanCodeChallenge(OAuthOptions authOptions) =>
    authOptions.UsePkce;

  static bool ShouldUseCodeChallenge(OAuthOptions authOptions) =>
    authOptions.UsePkce;
}