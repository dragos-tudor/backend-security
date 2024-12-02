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
    response.EnsureSuccessStatusCode();
    var contentType = GetHttpResponseContentType(response);
    var statusCode = GetHttpResponseStatusCode(response);

    if (contentType is null) return FormatString(MissingContentType, statusCode);
    if (!IsJsonContentTypeHttpResponse(contentType) &&
        !IsJwtContentTypeHttpResponse(contentType)) return FormatString(InvalidContentType, contentType, statusCode);
    return default;
  }

  static string? ValidateUserInfoResponse(
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken jwtIdToken,
    JwtPayload jwtUserToken)
  {
    var userTokenSub = GetJwtTokenPayloadSub(jwtUserToken);
    var idTokenSub = GetJwtTokenPayloadSub(jwtIdToken);

    if (IsEmptyString(userTokenSub))
      return MissingUserInfoSubClaim;

    if (IsEmptyString(idTokenSub))
      return MissingIdTokenSubClaim;

    if (!string.Equals(idTokenSub, userTokenSub))
      return FormatString(DifferentIdTokenUserTokenSubClaims, idTokenSub, userTokenSub!);

    return default;
  }
}