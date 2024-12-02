
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  public static async Task<UserInfoResult> AccessUserInfo<TOptions>(
    string accessToken,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions
  {
    using var request = BuildUserInfoRequest(oidcOptions.UserInfoEndpoint, accessToken);
    using var response = await SendHttpRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, oidcOptions, validationOptions, idToken, cancellationToken);
  }
}