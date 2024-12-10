using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string OidcSignOutSessionIdMissing = "oidc sign out session id is missing";
  const string OidcSignOutSessionIdInvalid = "oidc sign out session id is invalid";
  const string OidcSignOutIssuerMissing = "oidc sign out issuer is missing";
  const string OidcSignOutIssuerInvalid = "oidc sign out issuer is invalid";
  const string OidcSignOutPathMissing = "oidc sign out path is missing";

  static string? ValidateSignoutRequest(OidcData oidcData, OpenIdConnectOptions oidcOptions, ClaimsPrincipal? principal)
  {
    var sessionId = GetPrincipalClaimValue(principal, JwtRegisteredClaimNames.Sid);
    if (IsNotEmptyString(sessionId)) {
      var sid = GetOidcDataSid(oidcData);
      if (IsEmptyString(sid)) return OidcSignOutSessionIdMissing;
      if (!EqualsStringOrdinal(sid, sessionId)) return OidcSignOutSessionIdInvalid;
    }

    var issuer = GetPrincipalClaimValue(principal, JwtRegisteredClaimNames.Iss);
    if (IsNotEmptyString(issuer)) {
      var iss = GetOidcDataIss(oidcData);
      if (IsEmptyString(iss)) return OidcSignOutIssuerMissing;
      if (!EqualsStringOrdinal(iss, issuer)) return OidcSignOutIssuerInvalid;
    }

    if (oidcOptions.SignOutPath is null) return OidcSignOutPathMissing;
    return default;
  }
}