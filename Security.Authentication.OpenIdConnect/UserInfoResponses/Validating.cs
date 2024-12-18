using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string MissingContentType = "Unexpected response format. Content-Type header is missing. Status Code: {0}";
  const string InvalidContentType = "Unexpected response format. Content-Type {0}. Status Code: {1}.";
  const string MissingUserInfoSubClaim = "UserInfo Endpoint response does not contain a 'sub' claim, cannot validate.";
  const string MissingIdTokenSubClaim = "IdToken does not contain a 'sub' claim, cannot validate.";
  const string DifferentIdTokenUserTokenSubClaims = "Subject claim present in 'id_token': '{0}' does not match the claim received from UserInfo Endpoint: '{1}'.";

  static string? ValidateUserInfoResponse(HttpResponseMessage response)
  {
    var contentType = GetHttpResponseContentType(response);
    var statusCode = GetHttpResponseStatusCode(response);

    if (contentType is null) return MissingContentType.Format(statusCode);
    if (IsJsonContentTypeHttpResponse(contentType)) return default;
    if (IsJwtContentTypeHttpResponse(contentType)) return default;
    return InvalidContentType.Format(contentType, statusCode);
  }

  static string? ValidateUserInfoToken(
    JwtSecurityToken idToken,
    JwtPayload userToken)
  {
    var idTokenSub = GetJwtTokenPayloadSub(idToken);
    var userTokenSub = GetJwtTokenPayloadSub(userToken);

    if (IsEmptyString(userTokenSub)) return MissingUserInfoSubClaim;
    if (IsEmptyString(idTokenSub)) return MissingIdTokenSubClaim;
    if (!EqualsStringOrdinal(idTokenSub, userTokenSub)) return DifferentIdTokenUserTokenSubClaims.Format(idTokenSub, userTokenSub!);
    return default;
  }
}