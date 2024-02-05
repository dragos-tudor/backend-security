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
    if (authResult.Succeeded)
      await signin(context, authResult.Principal!, authResult.Properties!);

    if (authResult.Succeeded)
      return GetResponseLocation(context.Response);
    if (authResult.Failure is not null)
      return SetResponseRedirect(context.Response, BuildErrorPath(authOptions, authResult.Failure!));
    return default;
  }


  public static Task<string?> CallbackOAuth<TOptions> (
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OAuthOptions =>
      CallbackOAuth(
        context,
        ResolveService<TOptions>(context),
        authenticate,
        signin
      );
}