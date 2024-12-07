
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<AuthenticateResult> AuthenticateOidc<TOptions>(
    HttpContext context,
    ILogger logger)
  where TOptions: OpenIdConnectOptions =>
    AuthenticateOidc(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolveRequiredService<OpenIdConnectValidationOptions>(context),
      ResolveHttpClient(context),
      ResolvePropertiesDataFormat(context),
      PostAuthorize,
      ExchangeCodeForTokens,
      AccessUserInfo,
      logger
    );
}