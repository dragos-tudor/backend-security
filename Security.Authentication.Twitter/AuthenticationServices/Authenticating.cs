
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs {

  public static Task<AuthenticateResult> AuthenticateTwitterAsync (HttpContext context) =>
    AuthenticateOAuthAsync<TwitterOptions>(
      context,
      PostAuthorize<TwitterOptions>,
      ExchangeTwitterCodeForTokensAsync,
      AccessTwitterUserInfoAsync);

}