namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static bool ShouldCleanCodeChallenge(OAuthOptions oauthOptions) => oauthOptions.UsePkce;

  public static bool ShouldUseCodeChallenge(OAuthOptions oauthOptions) => oauthOptions.UsePkce && IsCodeResponseType(oauthOptions);
}