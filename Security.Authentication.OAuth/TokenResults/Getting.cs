
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal static string? GetAccessToken (TokenResult tokenResult) =>
    tokenResult.Success?.AccessToken;

  internal static string? GetExpiresIn (TokenResult tokenResult) =>
    tokenResult.Success?.ExpiresIn;

  internal static TokenInfo? GetTokenInfo (TokenResult tokenResult) =>
    tokenResult.Success;

  internal static string? GetTokenType (TokenResult tokenResult) =>
    tokenResult.Success?.TokenType;
}