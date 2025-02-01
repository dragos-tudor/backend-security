
using System.Net;
using Microsoft.AspNetCore.Http;
#nullable disable

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<string> CallbackOAuth<TOptions>(
    HttpContext context,
    TOptions oauthOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin,
    ILogger logger)
  where TOptions : OAuthOptions
  {
    var authResult = await authenticate(context);
    var authError = GetAuthenticateResultError(authResult);

    if (authError is not null) {
      LogCallbackOAuthWithFailure(logger, oauthOptions.SchemeName, authError, context.TraceIdentifier);
      SetHttpResponseStatus(context.Response, HttpStatusCode.InternalServerError);
      return default;
    }

    LogCallbackOAuth(logger, oauthOptions.SchemeName, context.TraceIdentifier);
    await signin(context, authResult.Principal!, authResult.Properties);

    var redirectUri = GetOAuthRedirectUri(authResult.Properties);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}