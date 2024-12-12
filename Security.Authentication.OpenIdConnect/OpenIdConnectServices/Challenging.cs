
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

    var callbackUrl = GetAbsoluteUrl(context.Request, oidcOptions.CallbackPath);
    var codeVerifier = ResolveCodeVerifier(oidcOptions);
    var redirectUri = GetAuthPropsRedirectUri(authProps) ?? GetHttpRequestQueryValue(context.Request, oidcOptions.ReturnUrlParameter);
    SetAuthorizationAuthProps(authProps, callbackUrl, correlationId, codeVerifier, redirectUri);

    var oidcParams = CreateOidcParams();
    var maxAge = GetOidcParamMaxAge(authProps, oidcOptions);
    var state = ProtectAuthProps(authProps, authPropsProtector);
    SetAuthorizationOidcParams(oidcParams, oidcOptions, authProps, callbackUrl, codeVerifier, maxAge, state);

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