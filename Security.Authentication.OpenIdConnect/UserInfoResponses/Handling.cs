using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<UserInfoResult> HandleUserInfoResponse<TOptions>(
    HttpResponseMessage response,
    TOptions oidcOptions,
    JwtSecurityToken idToken,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    if (!IsSuccessHttpResponse(response)) return await ReadJsonOAuthError(response, cancellationToken);

    var userInfoError = ValidateUserInfoResponse(response);
    if (userInfoError is not null) return userInfoError;

    var content = await ReadHttpResponseContent(response, cancellationToken);
    var contentType = GetHttpResponseContentType(response);

    var userToken = ReadUserInfoToken(content, contentType!);
    var validationError = ValidateUserInfoToken(idToken, userToken!);
    if (validationError is not null) return validationError;

    var claims = ApplyClaimMappers(oidcOptions.ClaimMappers, userToken!.Claims, GetClaimsIssuer(oidcOptions));
    return CreateUserInfoResult(claims);
  }
}