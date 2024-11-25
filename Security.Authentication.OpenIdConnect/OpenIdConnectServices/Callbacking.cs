using Microsoft.AspNetCore.Http;
#nullable disable

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string> CallbackOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    AuthenticateFunc authenticate,
    SignInFunc signin) where TOptions : OpenIdConnectOptions
  {
    var authResult = await authenticate(context);
    if (!authResult.Succeeded) {
      var failure = GetAuthenticateResultFailure(authResult);

      var redirectUriWithError = GetOAuthRedirectUriWithError(authResult.Properties, failure);
      return SetHttpResponseRedirect(context.Response, redirectUriWithError);
    }

    await signin(context, authResult.Principal, authResult.Properties);

    var redirectUri = GetOAuthRedirectUri(authResult.Properties!);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}