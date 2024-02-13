
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static UserInfoResult CreateFailureUserInfoResult (string error) =>
    new (default, error);

  static UserInfoResult CreateSuccessUserInfoResult (ClaimsPrincipal principal) =>
    new (principal, default);
}