
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static OidcTokens CreateOidcTokens(OidcData oidcData) =>
    new(
      GetOidcDataIdToken(oidcData),
      GetOidcDataAccessToken(oidcData),
      GetOidcDataTokenType(oidcData),
      GetOidcDataExpiresIn(oidcData)
    );
}