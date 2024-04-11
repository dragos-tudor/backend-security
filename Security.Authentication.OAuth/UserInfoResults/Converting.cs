
namespace Security.Authentication.OAuth;

partial record class UserInfoResult
{
  public static implicit operator UserInfoResult(string failure) =>
    CreateFailureUserInfoResult(failure);

  public static implicit operator UserInfoResult(ClaimsPrincipal principal) =>
    CreateSuccessUserInfoResult(principal);

  public static UserInfoResult ToUserInfoResult(string failure) => failure;

  public static UserInfoResult ToUserInfoResult(ClaimsPrincipal principal) => principal;
}