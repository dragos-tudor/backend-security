
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static Task<UserInfoResult> AccessTwitterUserInfo(
    HttpContext context,
    string accessToken,
    CancellationToken cancellationToken = default) =>
      AccessTwitterUserInfo(
        accessToken,
        ResolveRequiredService<TwitterOptions>(context),
        ResolveRequiredService<HttpClient>(context),
        cancellationToken
      );
}