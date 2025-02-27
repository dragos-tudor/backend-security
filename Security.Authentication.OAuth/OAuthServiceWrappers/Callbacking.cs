
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static Task<string> CallbackOAuth<TOptions>(
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin,
    ILogger logger)
  where TOptions : OAuthOptions =>
    CallbackOAuth(
      context,
      ResolveRequiredService<TOptions>(context),
      authenticate,
      signin,
      logger);
}