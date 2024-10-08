using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<string> CallbackOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin)
  where TOptions : OAuthOptions
  {
    var authResult = await authenticate(context);
    if (authResult.Succeeded) await signin(context, authResult.Principal!, authResult.Properties);
    if (!authResult.Succeeded) SetResponseRedirect(context.Response, BuildErrorPath(authOptions, authResult.Failure!));

    return GetCallbackRedirectUri(authResult.Properties!);
  }


  public static Task<string> CallbackOAuth<TOptions> (
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin)
  where TOptions : OAuthOptions =>
    CallbackOAuth(
      context,
      ResolveRequiredService<TOptions>(context),
      authenticate,
      signin);
}