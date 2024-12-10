
using Microsoft.AspNetCore.Http;
#nullable disable

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static async Task<string> CallbackOAuth<TOptions>(
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin)
  where TOptions : OAuthOptions
  {
    var authResult = await authenticate(context);
    var authError = GetAuthenticateResultError(authResult);

    if (authError is not null) {
      var redirectUriWithError = GetOAuthRedirectUri(authResult.Properties, authError);
      return SetHttpResponseRedirect(context.Response, redirectUriWithError);
    }

    await signin(context, authResult.Principal!, authResult.Properties);

    var redirectUri = GetOAuthRedirectUri(authResult.Properties);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}