
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static bool ExistsAccessToken(OAuthTokens tokens) => IsNotEmptyString(tokens.AccessToken);
}