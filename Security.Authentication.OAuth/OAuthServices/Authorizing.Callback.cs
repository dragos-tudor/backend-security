using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<string?> AuthorizeCallbackOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OAuthOptions
  {
    var authResult = await authenticate(context);
    if (authResult.Succeeded)
      await signin(context, authResult.Principal!, authResult.Properties!);

    var redirectUri = authResult.Succeeded switch {
      true => GetResponseLocation(context.Response),
      false => SetResponseRedirect(context.Response, BuildErrorPath(authOptions, authResult.Failure!))
    };
    return redirectUri ?? GetCallbackRedirectUri(authResult.Properties!);
  }


  public static Task<string?> AuthorizeCallbackOAuth<TOptions> (
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OAuthOptions =>
      AuthorizeCallbackOAuth(
        context,
        ResolveService<TOptions>(context),
        authenticate,
        signin
      );
}