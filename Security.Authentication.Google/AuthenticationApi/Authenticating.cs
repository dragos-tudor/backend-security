
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class Funcs {

  public static Task<AuthenticateResult> AuthenticateGoogleAsync (
    HttpContext context,
    GoogleOptions googleOptions) =>
      AuthenticateOAuthAsync(
        context,
        googleOptions,
        PostAuthorize<GoogleOptions>,
        ExchangeCodeForTokensAsync<GoogleOptions>,
        AccessUserInfoAsync);

}