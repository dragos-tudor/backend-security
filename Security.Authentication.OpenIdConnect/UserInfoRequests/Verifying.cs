using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool ShouldAccessUserInfo(
    OpenIdConnectOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    TokenInfo? tokenInfo) =>
      oidcOptions.GetClaimsFromUserInfoEndpoint &&
      IsNotEmptyString(tokenInfo?.AccessToken) &&
      IsNotEmptyString(oidcConfiguration.UserInfoEndpoint);
}