
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  public static async Task<UserInfoResult> AccessUserInfo<TOptions>(
    string accessToken,
    JwtSecurityToken securityToken,
    ClaimsIdentity identity,
    TOptions oidcOptions,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions
  {
    using var request = BuildUserInfoRequest(oidcOptions.UserInfoEndpoint, accessToken, httpClient.DefaultRequestVersion);
    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, identity, securityToken, oidcOptions, cancellationToken);
  }
}