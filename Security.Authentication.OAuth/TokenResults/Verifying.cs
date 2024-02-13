
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsAccessToken (TokenResult tokenResult) =>
    tokenResult.Success?.AccessToken is not null;
}