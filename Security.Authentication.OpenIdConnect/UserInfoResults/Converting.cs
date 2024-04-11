namespace Security.Authentication.OpenIdConnect;

partial record class UserInfoResult
{
  public static implicit operator UserInfoResult(string failure) =>
    new (default, failure);

  public static implicit operator UserInfoResult(ClaimsPrincipal principal) =>
    new (principal);

  public static UserInfoResult ToUserInfoResult(ClaimsPrincipal principal) => principal;

  public static UserInfoResult ToUserInfoResult(string failure) => failure;
}