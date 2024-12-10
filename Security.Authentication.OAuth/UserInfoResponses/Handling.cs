
using System.Net.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<UserInfoResult> HandleUserInfoResponse(
    HttpResponseMessage response,
    OAuthOptions oauthOptions,
    CancellationToken cancellationToken = default)
  {
    if (!IsSuccessHttpResponse(response)) return await ReadJsonOAuthError(response, cancellationToken);

    var rawClaims = await ReadHttpResponseJsonProps(response, cancellationToken);
    var claims = ApplyClaimMappers(oauthOptions.ClaimMappers, rawClaims, GetClaimsIssuer(oauthOptions));

    return CreateUserInfoResult(claims);
  }
}