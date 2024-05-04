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
    if (authResult.Succeeded) {
      await signin(context, authResult.Principal!, authResult.Properties!);
      return GetResponseLocation(context.Response) ??
        GetCallbackRedirectUri(authResult.Properties!);
    }

    var errorPath = BuildErrorPath(authOptions, authResult.Failure!);
    return SetResponseRedirect(context.Response, errorPath) ??
      GetCallbackRedirectUri(authResult.Properties!);
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