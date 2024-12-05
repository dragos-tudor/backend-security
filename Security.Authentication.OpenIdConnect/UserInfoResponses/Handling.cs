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

    var contentType = GetHttpResponseContentType(response);
    var content = await ReadHttpResponseContent(response, cancellationToken);

    if (!IsSuccessHttpResponse(response)) {
      using var errorResponse = JsonDocument.Parse(content);
      var errorDetails = errorResponse.RootElement;
      return GetOAuthErrorType(errorDetails);
    }

    var userToken = ParseUserInfoResponse(content, contentType);
    var validationError = ValidateUserInfoResponse(validationOptions, idToken, userToken!);
    if (validationError is not null) return validationError;

    var claims = ApplyClaimMappers(oidcOptions.ClaimMappers, userToken!.Claims, GetClaimsIssuer(oidcOptions));
    return claims.ToArray();
  }
}