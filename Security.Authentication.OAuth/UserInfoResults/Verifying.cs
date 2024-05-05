
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsFailureUserInfoResult(UserInfoResult userInfoResult) =>
    userInfoResult.Failure is not null;

  static bool IsSuccessUserInfoResult(UserInfoResult userInfoResult) =>
    userInfoResult.Success is not null;
}