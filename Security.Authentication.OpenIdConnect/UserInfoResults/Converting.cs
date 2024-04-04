namespace Security.Authentication.OpenIdConnect;

partial record class UserInfoResult
{
  public static implicit operator UserInfoResult(string failure) =>
    new (default, failure);

  public static implicit operator UserInfoResult(ClaimsPrincipal principal) =>
    new (principal);
}