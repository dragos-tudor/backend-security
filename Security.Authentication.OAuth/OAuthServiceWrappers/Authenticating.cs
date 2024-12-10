
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<AuthenticateResult> AuthenticateOAuth<TOptions>(
    HttpContext context,
    PostAuthorizeFunc<TOptions> postAuthorize,
    ExchangeCodeForTokensFunc<TOptions> exchangeCodeForTokens,
    AccessUserInfoFunc<TOptions> accessUserInfo,
    ILogger logger)
  where TOptions : OAuthOptions =>
    LogAuthentication(
      logger,
      await AuthenticateOAuth(
          context,
          ResolveRequiredService<TOptions>(context),
          ResolvePropertiesDataFormat(context),
          ResolveHttpClient(context),
          postAuthorize,
          exchangeCodeForTokens,
          accessUserInfo,
          logger),
      ResolveRequiredService<TOptions>(context).SchemeName,
      context.TraceIdentifier
    );
}