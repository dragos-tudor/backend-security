
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<TokenResult> ExchangeCodeForTokens<TOptions>(
    HttpContext context,
    string authCode,
    AuthenticationProperties authProps,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions =>
      ExchangeCodeForTokens(
        authCode,
        authProps,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectConfiguration>(context),
        ResolveStringDataFormat(context),
        ResolveHttpClient(context),
        GetHttpRequestCookies(context.Request),
        cancellationToken
      );

}