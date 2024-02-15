
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  internal static UserInfoResult CreateFailureUserInfoResult (string error) =>
    new (default, error);

  internal static UserInfoResult CreateSuccessUserInfoResult (ClaimsPrincipal principal) =>
    new (principal, default);
}