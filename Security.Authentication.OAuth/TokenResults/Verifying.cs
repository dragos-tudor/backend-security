
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsAccessToken (TokenResult tokenResult) =>
    tokenResult.TokenInfo?.AccessToken is not null;
}