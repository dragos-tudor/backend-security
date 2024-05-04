
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsFailedUserInfoResult(UserInfoResult userInfoResult) =>
    userInfoResult.Failure is not null;

  static bool IsSucceededUserInfoResult(UserInfoResult userInfoResult) =>
    userInfoResult.Success is not null;
}