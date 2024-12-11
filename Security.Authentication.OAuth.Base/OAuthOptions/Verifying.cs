
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool IsCodeResponseType(OAuthOptions oauthOptions) => EqualsStringOrdinal(oauthOptions.ResponseType, OAuthResponseType.Code);

  public static bool ShouldSaveTokens(OAuthOptions oauthOptions) => oauthOptions.SaveTokens;
}