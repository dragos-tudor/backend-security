
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs {

  public static Task<AuthenticateResult> AuthenticateFacebookAsync (HttpContext context) =>
    AuthenticateOAuthAsync<FacebookOptions>(
      context,
      PostAuthorize<FacebookOptions>,
      ExchangeCodeForTokensAsync<FacebookOptions>,
      AccessFacebookUserInfoAsync);

}