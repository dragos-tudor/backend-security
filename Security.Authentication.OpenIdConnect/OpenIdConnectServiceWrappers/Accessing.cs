
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<UserInfoResult> AccessUserInfo<TOptions>(
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
        ResolveHttpClient(context),
        cancellationToken
      );

}