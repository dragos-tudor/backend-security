
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static Task<UserInfoResult> AccessUserInfo<TOptions>(
    HttpContext context,
    string accessToken,
    TOptions authOptions,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    AccessUserInfo(
      accessToken,
      authOptions,
      ResolveHttpClient(context),
      cancellationToken
    );
}