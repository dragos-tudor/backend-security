
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<UserInfoResult> HandleUserInfoResponse(
    HttpResponseMessage response,
    OAuthOptions oauthOptions,
    CancellationToken cancellationToken = default)
  {
    using var userResponse = await ReadHttpResponseJsonContent(response, cancellationToken);
    var userData = userResponse.RootElement;

    if (!IsSuccessHttpResponse(response)) return GetOAuthErrorType(userData);

    var rawClaims = ToJsonDictionary(userData);
    var claims = ApplyClaimMappers(oauthOptions.ClaimMappers, rawClaims, GetClaimsIssuer(oauthOptions));

    return claims.ToArray();
  }
}