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

    if (authResult.Failure is not null) {
      var errorPath = BuildErrorPath(authOptions, authResult.Failure);
      return SetResponseRedirect(context.Response, errorPath);
    }
    if (authResult.Principal is null)
      return string.Empty;

    await signin(context, authResult.Principal, authResult.Properties!);

    var redirectUri = GetSigningRedirectUri(authResult.Properties!);
    return SetResponseRedirect(context.Response, redirectUri);
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