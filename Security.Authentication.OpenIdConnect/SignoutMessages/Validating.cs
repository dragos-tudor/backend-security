using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string RemoteSignOutSessionIdMissing = "remote sign out session id is missing";
  const string RemoteSignOutSessionIdInvalid = "remote sign out session id is invalid";

  const string RemoteSignOutIssuerMissing = "remote sign out issuer is missing";
  const string RemoteSignOutIssuerInvalid = "remote sign out issuer is invalid";

  static string? ValidateSignoutMessage(OpenIdConnectMessage oidcMessage, ClaimsPrincipal? principal)
  {
    var sessionId = GetPrincipalClaimValue(principal, JwtRegisteredClaimNames.Sid);
    if (IsEmptyString(sessionId)) return RemoteSignOutSessionIdMissing;
    if (oidcMessage.Sid != sessionId) return RemoteSignOutSessionIdInvalid;

    var issuer = GetPrincipalClaimValue(principal, JwtRegisteredClaimNames.Iss);
    if (IsEmptyString(issuer)) return RemoteSignOutIssuerMissing;
    if (oidcMessage.Iss != issuer) return RemoteSignOutIssuerInvalid;

    return default;
  }
}