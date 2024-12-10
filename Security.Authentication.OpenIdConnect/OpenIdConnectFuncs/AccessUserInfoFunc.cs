
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<UserInfoResult> AccessUserInfoFunc<TOptions>(
    string accessToken,
    TOptions oidcOptions,
    JwtSecurityToken idToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions;