using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<UserInfoResult> HandleUserInfoResponse<TOptions>(
    HttpResponseMessage response,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    var userInfoError = ValidateUserInfoResponse(response);
    if (userInfoError is not null) return userInfoError;

    var responseContent = await ReadHttpResponseContent(response, cancellationToken);
    var contentType = GetHttpResponseContentType(response);

    var userToken = ParseUserInfoData(responseContent, contentType);

    var validationError = ValidateUserInfoResponse(validationOptions, idToken, userToken!);
    if (!IsSuccessHttpResponse(response)) return GetOAuthErrorType(userToken);

    var rawClaims = ToJsonDictionary(userToken);
    var mappedClaims = ApplyClaimMappers(oidcOptions.ClaimMappers, rawClaims, GetClaimsIssuer(oidcOptions));
    var claims = ApplyClaimActions(oidcOptions.ClaimActions, mappedClaims);

    return claims.ToArray();
  }
}