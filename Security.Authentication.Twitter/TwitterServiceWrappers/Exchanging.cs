
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Security.Authentication.OAuth;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  public static Task<TokenResult> ExchangeTwitterCodeForTokens(
    HttpContext context,
    string authCode,
    AuthenticationProperties authProps,
    CancellationToken cancellationToken = default) =>
      ExchangeCodeForTokens(
        authCode,
        authProps,
        ResolveRequiredService<TwitterOptions>(context),
        ResolveRequiredService<HttpClient>(context),
        cancellationToken
      );

}