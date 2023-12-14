
namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  static UserInfoResult CreateFailureUserInfoResult (string error) =>
    new (default, error);

  static UserInfoResult CreateSuccessUserInfoResult (ClaimsPrincipal principal) =>
    new (principal, default);

}