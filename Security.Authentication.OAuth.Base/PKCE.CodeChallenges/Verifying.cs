namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static bool ShouldCleanCodeChallenge(OAuthOptions authOptions) => authOptions.UsePkce;

  public static bool ShouldUseCodeChallenge(OAuthOptions authOptions) => authOptions.UsePkce && IsCodeResponseType(authOptions);
}