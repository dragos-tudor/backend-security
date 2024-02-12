
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  public static string ChallengeRemoteOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat propertiesDataFormat,
    DateTimeOffset currentUtc)
  where TOptions: OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    UseCorrelationCookie(context, correlationId, authOptions, currentUtc);

    var authProperties = CreateAuthenticationProperties();
    var authParams = CreateAuthorizationParams();
    if (ShouldUseCodeChallenge(authOptions))
      UseCodeChallenge(authProperties, authParams, GenerateCodeVerifier());

    var callbackUrl = BuildAbsoluteUrl(context.Request, authOptions.CallbackPath);
    var redirectUri = GetRequestQueryReturnUrl(context.Request, authOptions.ReturnUrlParameter)!;
    SetChallengeAuthenticationProperties(authProperties, callbackUrl, correlationId, redirectUri);
    SetChallengeAuthorizationParams(authParams, authOptions, callbackUrl,
      ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    var authUri = GetAuthorizationUri(authOptions, authParams);
    LogChallengedRemote(Logger, authOptions.SchemeName, authUri, context.TraceIdentifier);

    return SetResponseRedirect(context.Response, authUri)!;
  }

  public static string ChallengeRemoteOAuth<TOptions> (HttpContext context) where TOptions: OAuthOptions =>
    ChallengeRemoteOAuth(
      context,
      ResolveService<TOptions>(context),
      ResolveService<PropertiesDataFormat>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

}