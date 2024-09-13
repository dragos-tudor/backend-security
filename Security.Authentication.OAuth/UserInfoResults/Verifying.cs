
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool IsUserInfoResultFailure(UserInfoResult userInfoResult) => userInfoResult.Failure is not null;

  static bool IsUserInfoResultSuccess(UserInfoResult userInfoResult) => userInfoResult.Success is not null;
}