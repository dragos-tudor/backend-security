
namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static ClaimsPrincipal? GetClaimsPrincipal (UserInfoResult userInfoResult) =>
    userInfoResult.Success;
}