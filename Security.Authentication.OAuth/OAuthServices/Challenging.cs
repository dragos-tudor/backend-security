
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static string ChallengeOAuth<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions authOptions,
    DateTimeOffset currentUtc,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions: OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    UseCorrelationCookie(context, authOptions, correlationId, currentUtc);
    SetAuthPropsCorrelationId(authProps, correlationId);

    var authParams = CreateOAuthParams();
    if(ShouldUseCodeChallenge(authOptions)) UseCodeChallenge(authParams, authProps, GenerateCodeVerifier());

    var redirectUri = GetHttpRequestQueryValue(context.Request, authOptions.ReturnUrlParameter)!; // TODO: investigate security risk for absolute url
    if(!ExistsAuthPropsRedirectUri(authProps)) SetAuthPropsRedirectUri(authProps, redirectUri);

    var callbackUrl = GetAbsoluteUrl(context.Request, authOptions.CallbackPath);
    SetAuthorizationOAuthParams(authParams, authOptions, authProps, authPropsProtector, callbackUrl);
    SetOAuthParams(authParams, authOptions.AdditionalAuthorizationParameters);

    var authUri = BuildHttpRequestUri(authOptions.AuthorizationEndpoint, authParams!);
    SetHttpResponseRedirect(context.Response, authUri);

    LogAuthorizeChallenge(logger, authOptions.SchemeName, GetHttpResponseLocation(context.Response)!, GetHttpResponseSetCookie(context.Response)!, context.TraceIdentifier);
    return authUri;
  }
}