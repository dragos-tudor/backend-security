
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string ChallengeRemoteOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    PropertiesDataFormat propertiesDataFormat,
    DateTimeOffset currentUtc)
  where TOptions: OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    UseCorrelationCookie(context, correlationId, authOptions, currentUtc);

    var callbackUrl = BuildAbsoluteUrl(context.Request, authOptions.CallbackPath);
    var authProperties = BuildAuthenticationProperties(context, authOptions, callbackUrl, correlationId);
    if (ShouldUseCodeChallenge(authOptions))
      SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    var state = ProtectAuthenticationProperties(authProperties, propertiesDataFormat);
    var authParams = BuildAuthorizationParams(authProperties, authOptions, callbackUrl, state);
    if (ShouldUseCodeChallenge(authOptions))
      SetAuthorizationParamsCodeChallenge(authParams, authProperties);

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