using Microsoft.AspNetCore.Http;
#nullable disable

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string> CallbackOidc<TOptions>(
    HttpContext context,
    AuthenticateFunc authenticate,
    SignInFunc signin)
  where TOptions : OpenIdConnectOptions
  {
    var authResult = await authenticate(context);
    var authError = GetAuthenticateResultError(authResult);

    if (authError is not null) {
      var redirectUriWithError = GetOAuthRedirectUriWithError(authResult.Properties, authError);
      return SetHttpResponseRedirect(context.Response, redirectUriWithError);
    }

    await signin(context, authResult.Principal, authResult.Properties);

    var redirectUri = GetOAuthRedirectUri(authResult.Properties!);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}