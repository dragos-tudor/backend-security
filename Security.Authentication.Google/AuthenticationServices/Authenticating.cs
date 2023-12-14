
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs {

  public static Task<AuthenticateResult> AuthenticateGoogleAsync (HttpContext context) =>
    AuthenticateOAuthAsync<GoogleOptions>(
      context,
      PostAuthorize<GoogleOptions>,
      ExchangeCodeForTokensAsync<GoogleOptions>,
      AccessUserInfoAsync);

}