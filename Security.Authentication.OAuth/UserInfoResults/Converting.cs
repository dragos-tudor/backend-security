
namespace Security.Authentication.OAuth;

partial record class UserInfoResult
{
  public static implicit operator UserInfoResult(string failure) =>
    CreateFailureUserInfoResult(failure);

  public static implicit operator UserInfoResult(ClaimsPrincipal principal) =>
    CreateSuccessUserInfoResult(principal);
}