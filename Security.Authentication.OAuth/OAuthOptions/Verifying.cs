namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ShouldUseCodeChallenge(OAuthOptions authOptions) =>
    authOptions.UsePkce;
}