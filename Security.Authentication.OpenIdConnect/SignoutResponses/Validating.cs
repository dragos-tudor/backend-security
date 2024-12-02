using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string OidcSignOutSessionIdMissing = "oidc sign out session id is missing";
  const string OidcSignOutSessionIdInvalid = "oidc sign out session id is invalid";

  const string OidcSignOutIssuerMissing = "oidc sign out issuer is missing";
  const string OidcSignOutIssuerInvalid = "oidc sign out issuer is invalid";

  static string? ValidateSignoutResponse(OidcData oidcData, ClaimsPrincipal? principal)
  {
    var sessionId = GetPrincipalClaimValue(principal, JwtRegisteredClaimNames.Sid);
    if (IsEmptyString(sessionId)) return OidcSignOutSessionIdMissing;
    if (GetOidcDataSid(oidcData) != sessionId) return OidcSignOutSessionIdInvalid;

    var issuer = GetPrincipalClaimValue(principal, JwtRegisteredClaimNames.Iss);
    if (IsEmptyString(issuer)) return OidcSignOutIssuerMissing;
    if (GetOidcDataIss(oidcData) != issuer) return OidcSignOutIssuerInvalid;

    return default;
  }
}