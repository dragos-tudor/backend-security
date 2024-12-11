
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async ValueTask<string?> ChallengeOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    DateTimeOffset currentUtc,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var correlationId = GenerateCorrelationId();
    UseCorrelationCookie(context, oidcOptions, correlationId, currentUtc);
    SetAuthPropsCorrelationId(authProps, correlationId);

    var oidcParams = CreateOidcParams();
    if (ShouldUseCodeChallenge(oidcOptions)) UseCodeChallenge(oidcParams, authProps, GenerateCodeVerifier());

    var callbackUrl = GetAbsoluteUrl(context.Request, oidcOptions.CallbackPath);
    var redirectUri = GetHttpRequestQueryValue(context.Request, oidcOptions.ReturnUrlParameter)!;
    SetAuthorizationAuthProps(authProps, redirectUri, callbackUrl);

    var state = authPropsProtector.Protect(authProps);
    SetAuthorizationOidcParams(oidcParams, authProps, oidcOptions, state, callbackUrl);
    SetAdditionalOAuthParams(oidcParams, oidcOptions.AdditionalAuthorizationParameters);

    if (IsRedirectGetAuthMethod(oidcOptions))
      SetHttpResponseRedirect(context.Response, BuildHttpRequestUri(oidcOptions.AuthorizationEndpoint, oidcParams!));

    if (IsFormPostAuthMethod(oidcOptions))
    {
      ResetHttpResponseCacheHeaders(context.Response);
      await WriteHttpResponseTextContent(context.Response, BuildHttpRequestFormPost(oidcOptions.AuthorizationEndpoint, oidcParams), context.RequestAborted);
    }

    LogAuthorizeChallenge(logger, oidcOptions.SchemeName, GetHttpResponseLocation(context.Response)!, GetHttpResponseSetCookie(context.Response)!, context.TraceIdentifier);
    return oidcOptions.AuthorizationEndpoint;
  }

  // TODO: validate OpenIdConnectOptions [request type == authorization code flow, absolute uri == AuthorizationEndpoint, scopes supported, code challenge method supported]
  // TODO: implement PAR support [https://datatracker.ietf.org/doc/html/rfc9126]
}