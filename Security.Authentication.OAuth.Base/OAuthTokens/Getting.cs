
namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string? GetAccessToken(OAuthTokens oauthTokens) => oauthTokens.AccessToken;

  public static string? GetRefreshToken(OAuthTokens oauthTokens) => oauthTokens.RefreshToken;

  public static string? GetExpiresIn(OAuthTokens oauthTokens) => oauthTokens.ExpiresIn;

  public static string? GetTokenType(OAuthTokens oauthTokens) => oauthTokens.TokenType;
}