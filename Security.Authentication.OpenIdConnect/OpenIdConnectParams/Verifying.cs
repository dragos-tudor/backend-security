
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsOpenIdConnectCodeFlow(OidcParams oidcParams) => IsNotEmptyString(oidcParams[AuthorizationCode]);
}