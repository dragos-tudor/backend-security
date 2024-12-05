
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  static bool IsCodeResponseType(OAuthOptions oauthOptions) => string.Equals(oauthOptions.ResponseType, OAuthResponseType.Code, StringComparison.Ordinal);

  public static bool ShouldSaveTokens(OAuthOptions oauthOptions) => oauthOptions.SaveTokens;
}