
using System.Net;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> CallbackSignOut<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector,
    PostSignoutFunc<TOptions> postSignOut,
    SignOutFunc signOut,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var (authProps, signoutError) = postSignOut(context, oidcOptions, authPropsProtector);

    if (signoutError is not null) {
      LogCallbackSignOutWithFailure(logger, oidcOptions.SchemeName, ToOAuthErrorString(signoutError), context.TraceIdentifier);
      SetHttpResponseStatus(context.Response, HttpStatusCode.InternalServerError);
      return default;
    }

    LogCallbackSignOut(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    await signOut(context);

    var redirectUri = GetOAuthRedirectUri(authProps!);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}