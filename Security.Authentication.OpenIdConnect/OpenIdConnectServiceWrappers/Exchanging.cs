
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<TokenResult> ExchangeCodeForTokens<TOptions>(
    HttpContext context,
    string code,
    AuthenticationProperties authProps,
    CancellationToken cancellationToken = default)
  where TOptions: OpenIdConnectOptions =>
      ExchangeCodeForTokens(
        code,
        authProps,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectValidationOptions>(context),
        ResolveHttpClient(context),
        cancellationToken
      );

}