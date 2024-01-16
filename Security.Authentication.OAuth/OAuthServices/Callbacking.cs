using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static async Task<string?> CallbackOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    Func<HttpContext, Task<AuthenticateResult>> authenticate,
    SignInFunc signin)
  where TOptions : OAuthOptions
  {
    var authResult = await authenticate(context);
    if (authResult.Failure is not null) {
      var errorPath = BuildErrorPath(authOptions, authResult.Failure);
      return SetResponseRedirect(context.Response, errorPath);
    }

    if (authResult.Principal is not null) {
      await signin(context, authResult.Principal, authResult.Properties!);
      var redirectUri = GetSigningRedirectUri(authResult.Properties!);
      return SetResponseRedirect(context.Response, redirectUri);
    }

    return string.Empty;
  }


  public static Task<string?> CallbackOAuth<TOptions> (
    HttpContext context,
    Func<HttpContext, Task<AuthenticateResult>> authenticate,
    SignInFunc signin)
  where TOptions : OAuthOptions =>
      CallbackOAuth(
        context,
        ResolveService<TOptions>(context),
        authenticate,
        signin
      );
}