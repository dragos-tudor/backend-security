
namespace Security.Authentication.OAuth;

partial class Funcs {

  static UserInfoResult CreateFailureUserInfoResult (string error) =>
    new (default, error);

  static UserInfoResult CreateSuccessUserInfoResult (ClaimsPrincipal principal) =>
    new (principal, default);

}