
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs {

  public static async Task<UserInfoResult> AccessUserInfo<TOptions> (
    string accessToken,
    JwtSecurityToken securityToken,
    ClaimsIdentity identity,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions
  {
    using var request = BuildUserInfoRequest(oidcConfiguration.TokenEndpoint, accessToken, httpClient.DefaultRequestVersion);
    using var response = await SendUserInfoRequest(request, httpClient, cancellationToken);
    return await HandleUserInfoResponse(response, identity, securityToken, oidcOptions, cancellationToken);
  }

  public static Task<UserInfoResult> AccessUserInfo<TOptions> (
    HttpContext context,
    string accessToken,
    JwtSecurityToken securityToken,
    ClaimsIdentity identity,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions =>
      AccessUserInfo(
        accessToken,
        securityToken,
        identity,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectConfiguration>(context),
        ResolveHttpClient<TOptions>(context),
        cancellationToken
      );

}