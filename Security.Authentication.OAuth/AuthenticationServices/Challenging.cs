
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class Funcs {

  public static string ChallengeOAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions,
    DateTimeOffset currentUtc) where TOptions: OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    var callbackUri = BuildAbsoluteUri(context.Request, authOptions.CallbackPath);
    SetupCorrelationCookie(context, authOptions, currentUtc, correlationId);
    SetAuthenticationPropertiesCorrelationId(authProperties, correlationId);
    SetAuthenticationPropertiesRedirectUri(authProperties, GetRequestQueryReturnUrl(context.Request, authOptions.ReturnUrlParameter)!);
    SetAuthenticationPropertiesCallbackUri(authProperties, callbackUri);
    if (authOptions.UsePkce) SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    var state = ProtectAuthenticationProperties(authProperties, authOptions.StateDataFormat);
    var authParams = CreateAuthorizationParams(authOptions.ClientId, FormatOAuthScopes(authOptions), state, callbackUri);
    if (authOptions.UsePkce) AddAuthorizationCodeChallengeParams(authParams, HashCodeVerifier(GetAuthenticationPropertiesCodeVerifier(authProperties)!));

    var authUri = BuildAuthorizationUri(authOptions, authParams);
    LogChallenged(Logger, authOptions.SchemeName, authUri, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, authUri)!;
  }

}