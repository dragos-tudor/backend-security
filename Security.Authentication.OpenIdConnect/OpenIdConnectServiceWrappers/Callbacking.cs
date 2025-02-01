
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static Task<string> CallbackOidc<TOptions>(
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    CallbackOidc(
      context,
      ResolveRequiredService<TOptions>(context),
      authenticate,
      signin,
      logger
    );
}