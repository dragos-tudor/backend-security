using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

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
    var (authProps, signoutError) = await postSignOut(context, oidcOptions, authPropsProtector);

    if (signoutError is not null) {
      LogSignedOutWithFailure(logger, oidcOptions.SchemeName, signoutError, context.TraceIdentifier);

      var redirectUriWithError = GetOAuthRedirectUriWithError(CreateAuthProps(), signoutError);
      return SetHttpResponseRedirect(context.Response, redirectUriWithError);
    }

    await signOut(context);

    var redirectUri = GetOAuthRedirectUri(authProps!);
    LogSignedOut(logger, oidcOptions.SchemeName, redirectUri, context.TraceIdentifier);
    return SetHttpResponseRedirect(context.Response, redirectUri);
  }
}