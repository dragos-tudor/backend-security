namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static ClaimsPrincipal GetUserInfoResultPrincipal(UserInfoResult userInfo) =>
    userInfo.Success!;
}