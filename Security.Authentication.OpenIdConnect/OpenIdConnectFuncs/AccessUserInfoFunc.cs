
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading;

namespace Security.Authentication.OpenIdConnect;

public delegate Task<UserInfoResult> AccessUserInfoFunc<TOptions>(string accessToken, JwtSecurityToken securityToken, ClaimsIdentity identity, TOptions oidcOptions, HttpClient httpClient, CancellationToken cancellationToken = default) where TOptions: OpenIdConnectOptions;