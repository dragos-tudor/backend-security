
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcTokens CreateOidcTokens(OidcData oidcData) =>
    new(
      GetOidcDataIdToken(oidcData),
      GetOidcDataAccessToken(oidcData),
      GetOidcDataRefreshToken(oidcData),
      GetOidcDataTokenType(oidcData),
      GetOidcDataExpiresIn(oidcData)
    );
}