
#nullable disable

using System.Net;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string> CallbackOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var authResult = await authenticate(context);
    var authError = GetAuthenticateResultError(authResult);

    if (authError is not null) {
      LogCallbackOAuthWithFailure(logger, oidcOptions.SchemeName, authError, context.TraceIdentifier);
      SetHttpResponseStatus(context.Response, HttpStatusCode.InternalServerError);
      return default;
    }

    LogCallbackOAuth(logger, oidcOptions.SchemeName, context.TraceIdentifier);
    await signin(context, authResult.Principal, authResult.Properties);

    var redirectUri = GetOAuthRedirectUri(authResult.Properties!);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}