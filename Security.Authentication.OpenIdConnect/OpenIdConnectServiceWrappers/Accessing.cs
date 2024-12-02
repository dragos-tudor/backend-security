
using System.IdentityModel.Tokens.Jwt;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<UserInfoResult> AccessUserInfo<TOptions>(
    HttpContext context,
    string accessToken,
    JwtSecurityToken idToken,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions =>
      AccessUserInfo(
        accessToken,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectValidationOptions>(context),
        idToken,
        ResolveHttpClient(context),
        cancellationToken
      );

}