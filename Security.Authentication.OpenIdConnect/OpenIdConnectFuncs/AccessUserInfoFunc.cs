
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<UserInfoResult> AccessUserInfoFunc<TOptions>(
    string accessToken,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    JwtSecurityToken idToken,
    HttpClient httpClient,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions;