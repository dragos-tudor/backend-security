using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string UnableToParseUserInfoData = "Unable to parse oidc user info data.";

  static async Task<UserInfoResult> HandleUserInfoResponse<TOptions>(
    HttpResponseMessage response,
    ClaimsIdentity identity,
    JwtSecurityToken securityToken,
    TOptions oidcOptions,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    var userInfoError = ValidateUserInfoResponse(response);
    if(userInfoError is not null) return userInfoError;

    var responseContent = await ReadTokenResponseContent(response, cancellationToken);
    ValidateUserInfoMessageProtocol(responseContent, securityToken);

    var contentType = GetUserInfoResponseContentType(response);
    var userData = ParseUserInfoData(responseContent, contentType);
    if (userData is null) return UnableToParseUserInfoData;
    using (userData)
      AddClaimsIdentityClaims(identity, userData, oidcOptions);

    return CreatePrincipal(identity);
  }
}