
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class Funcs {

  public static Task<AuthenticateResult> AuthenticateFacebookAsync (
    HttpContext context,
    FacebookOptions facebookOptions) =>
      AuthenticateOAuthAsync(
        context,
        facebookOptions,
        PostAuthorize<FacebookOptions>,
        ExchangeCodeForTokensAsync<FacebookOptions>,
        AccessFacebookUserInfoAsync);

}