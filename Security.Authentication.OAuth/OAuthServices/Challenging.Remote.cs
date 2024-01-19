
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
    var cookieOptions = BuildCorrelationCookie(context, authOptions, currentUtc);
    AppendCorrelationCookie(context.Response, GetCorrelationCookieName(correlationId), cookieOptions);

    var callbackUrl = BuildAbsoluteUrl(context.Request, authOptions.CallbackPath);
    var authProperties = new AuthenticationProperties();
    SetAuthenticationPropertiesCorrelationId(authProperties, correlationId);
    SetAuthenticationPropertiesRedirectUri(authProperties, GetRequestQueryReturnUrl(context.Request, authOptions.ReturnUrlParameter)!);
    SetAuthenticationPropertiesCallbackUri(authProperties, callbackUrl);
    if (authOptions.UsePkce) SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    var state = ProtectAuthenticationProperties(authProperties, propertiesDataFormat);
    var authParams = CreateAuthorizationParams(authOptions.ClientId, FormatOAuthScopes(authOptions), state, callbackUrl);
    if (authOptions.UsePkce) AddAuthorizationCodeChallengeParams(authParams, HashCodeVerifier(GetAuthenticationPropertiesCodeVerifier(authProperties)!));

    var authUri = BuildAuthorizationUri(authOptions, authParams);
    LogChallenged(Logger, authOptions.SchemeName, authUri, context.TraceIdentifier);
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