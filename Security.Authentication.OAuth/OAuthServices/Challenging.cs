
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static string ChallengeOAuth<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oauthOptions,
    DateTimeOffset currentUtc,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions: OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    UseCorrelationCookie(context, oauthOptions, correlationId, currentUtc);
    SetAuthPropsCorrelationId(authProps, correlationId);

    var oauthParams = CreateOAuthParams();
    if (ShouldUseCodeChallenge(oauthOptions)) UseCodeChallenge(oauthParams, authProps, GenerateCodeVerifier());

    var redirectUri = GetHttpRequestQueryValue(context.Request, oauthOptions.ReturnUrlParameter)!; // TODO: investigate security risk for absolute url
    if (!ExistsAuthPropsRedirectUri(authProps)) SetAuthPropsRedirectUri(authProps, redirectUri);

    var callbackUrl = GetAbsoluteUrl(context.Request, oauthOptions.CallbackPath);
    SetAuthorizationOAuthParams(oauthParams, oauthOptions, authProps, authPropsProtector, callbackUrl);
    SetOAuthParams(oauthParams, oauthOptions.AdditionalAuthorizationParameters);

    var authUri = BuildHttpRequestUri(oauthOptions.AuthorizationEndpoint, oauthParams!);
    SetHttpResponseRedirect(context.Response, authUri);

    LogAuthorizeChallenge(logger, oauthOptions.SchemeName, GetHttpResponseLocation(context.Response)!, GetHttpResponseSetCookie(context.Response)!, context.TraceIdentifier);
    return authUri;
  }
}