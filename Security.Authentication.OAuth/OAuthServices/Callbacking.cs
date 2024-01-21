using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<string?> CallbackOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OAuthOptions
  {
    var authResult = await authenticate(context);
    if (authResult.None) return string.Empty;
    if (!authResult.Succeeded)
      return SetResponseRedirect(context.Response, BuildErrorPath(authOptions, authResult.Failure!));

    await signin(context, authResult.Principal, authResult.Properties!);
    return SetResponseRedirect(context.Response, GetCallbackRedirectUri(authResult.Properties!));
  }


  public static Task<string?> CallbackOAuth<TOptions> (
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin)
  where TOptions : OAuthOptions =>
      CallbackOAuth(
        context,
        ResolveService<TOptions>(context),
        authenticate,
        signin
      );
}