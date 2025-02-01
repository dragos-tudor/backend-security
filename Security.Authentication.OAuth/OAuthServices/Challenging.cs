
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
  where TOptions : OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    UseCorrelationCookie(context, oauthOptions, correlationId, currentUtc);

    var callbackUrl = GetAbsoluteUrl(context.Request, oauthOptions.CallbackPath);
    var codeVerifier = ResolveCodeVerifier(oauthOptions);
    var redirectUri = GetAuthPropsRedirectUri(authProps) ?? GetHttpRequestQueryValue(context.Request, oauthOptions.ReturnUrlParameter);
    SetChallengeAuthProps(authProps, correlationId, codeVerifier, callbackUrl, redirectUri);

    var oauthParams = CreateOAuthParams();
    var state = ProtectAuthProps(authProps, authPropsProtector);
    SetChallengeOAuthParams(oauthParams, oauthOptions, callbackUrl, codeVerifier, state);

    var authUri = BuildHttpRequestUri(oauthOptions.AuthorizationEndpoint, oauthParams!);
    SetHttpResponseRedirect(context.Response, authUri);

    LogChallengeOAuth(logger, oauthOptions.SchemeName, GetHttpResponseLocation(context.Response)!, GetHttpResponseSetCookie(context.Response)!, context.TraceIdentifier);
    return authUri;
  }
}