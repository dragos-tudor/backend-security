
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<bool> ChallengeSignOut<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var challengeRequest = await GetHttpRequestParams(context.Request, context.RequestAborted);
    if (challengeRequest is null) return false;

    var challengeData = ToOpenIdConnectData(challengeRequest);
    var validationError = ValidateSignoutRequest(challengeData, oidcOptions, GetContextUser(context));
    if (validationError is not null) {
      LogChallengeSignOutWithFailure(logger, oidcOptions.SchemeName, validationError, context.TraceIdentifier);
      return false;
    }
    SetChallengeSignoutAuthProps(authProps, oidcOptions.CallbackSignOutPath);

    var oidcParams = CreateOidcParams();
    var idTokenHint = await GetOidcParamIdTokenHint(context, oidcOptions); // TODO: validate required id token hint
    var redirectUri = GetAbsoluteUrl(context.Request, oidcOptions.CallbackSignOutPath);
    var state = ProtectAuthProps(authProps, authPropsProtector);
    SetChallengeSignoutParams(oidcParams, oidcOptions, idTokenHint!, redirectUri, state);

    if (IsRedirectGetAuthMethod(oidcOptions))
      SetHttpResponseRedirect(context.Response, BuildHttpRequestUri(oidcOptions.SignOutPath!, oidcParams!));

    if (IsFormPostAuthMethod(oidcOptions))
    {
      ResetHttpResponseCacheHeaders(context.Response);
      await WriteHttpResponseTextContent(context.Response, BuildHttpRequestFormPost(oidcOptions.SignOutPath!, oidcParams!), context.RequestAborted);
    }

    LogChallengeSignOut(logger, oidcOptions.SchemeName, GetHttpResponseLocation(context.Response)!, GetHttpResponseSetCookie(context.Response)!, context.TraceIdentifier);
    return true;
  }
}