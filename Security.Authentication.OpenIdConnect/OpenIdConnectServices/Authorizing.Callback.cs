using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> AuthorizeCallbackOidc<TOptions> (
    HttpContext context,
    TOptions oidcOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OpenIdConnectOptions
  {
    var authResult = await authenticate(context);
    if (authResult.Succeeded)
      await signin(context, authResult.Principal!, authResult.Properties!);

    var redirectUri = authResult.Succeeded switch {
      true => GetResponseLocation(context.Response),
      false => SetResponseRedirect(context.Response, BuildErrorPath(oidcOptions, authResult.Failure!))
    };
    return redirectUri ?? GetCallbackRedirectUri(authResult.Properties!);
  }

  public static Task<string?> AuthorizeCallbackOidc<TOptions> (
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OpenIdConnectOptions =>
      AuthorizeCallbackOidc(
        context,
        ResolveService<TOptions>(context),
        authenticate,
        signin
      );
}