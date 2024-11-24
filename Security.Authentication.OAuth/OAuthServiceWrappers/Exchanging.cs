
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static Task<TokenResult> ExchangeCodeForTokens<TOptions>(
    HttpContext context,
    string authCode,
    AuthenticationProperties authProps,
    CancellationToken cancellationToken = default)
  where TOptions: OAuthOptions =>
    ExchangeCodeForTokens(
      authCode,
      authProps,
      ResolveRequiredService<TOptions>(context),
      ResolveHttpClient(context),
      cancellationToken
    );
}