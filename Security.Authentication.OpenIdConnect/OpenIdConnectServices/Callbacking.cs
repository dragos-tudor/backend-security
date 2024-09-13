using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string> CallbackOidc<TOptions> (
    HttpContext context,
    TOptions oidcOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OpenIdConnectOptions
  {
    var authResult = await authenticate(context);
    if (authResult.Succeeded) await signin(context, authResult.Principal!, authResult.Properties);
    if (!authResult.Succeeded) SetResponseRedirect(context.Response, BuildErrorPath(oidcOptions, authResult.Failure!));

    return GetCallbackRedirectUri(authResult.Properties!);
  }

  public static Task<string> CallbackOidc<TOptions> (
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OpenIdConnectOptions =>
      CallbackOidc(
        context,
        ResolveRequiredService<TOptions>(context),
        authenticate,
        signin
      );
}