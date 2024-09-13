
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistsAccessToken (TokenResult tokenResult) => tokenResult.Success?.AccessToken is not null;

  static bool IsTokenResultFailure (TokenResult tokenResult) => tokenResult.Failure is not null;

  static bool IsTokenResultSucceess (TokenResult tokenResult) => tokenResult.Success is not null;
}