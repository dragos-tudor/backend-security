
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static Task<UserInfoResult> AccessFacebookUserInfo(
    HttpContext context,
    string accessToken,
    FacebookOptions facebookOptions,
    CancellationToken cancellationToken = default) =>
      AccessFacebookUserInfo(
        accessToken,
        facebookOptions,
        ResolveRequiredService<HttpClient>(context),
        cancellationToken
      );

}