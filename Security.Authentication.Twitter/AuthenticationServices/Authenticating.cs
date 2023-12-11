
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Twitter;

partial class Funcs {

  public static Task<AuthenticateResult> AuthenticateTwitterAsync (
    HttpContext context,
    TwitterOptions twitterOptions) =>
      AuthenticateOAuthAsync(
        context,
        twitterOptions,
        PostAuthorize<TwitterOptions>,
        ExchangeTwitterCodeForTokensAsync,
        AccessTwitterUserInfoAsync);

}