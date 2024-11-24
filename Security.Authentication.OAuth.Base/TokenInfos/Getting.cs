
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetAccessToken(TokenInfo tokenInfo) => tokenInfo.AccessToken;

  public static string? GetRefreshToken(TokenInfo tokenInfo) => tokenInfo.RefreshToken;

  public static string? GetExpiresIn(TokenInfo tokenInfo) => tokenInfo.ExpiresIn;

  public static string? GetTokenType(TokenInfo tokenInfo) => tokenInfo.TokenType;
}