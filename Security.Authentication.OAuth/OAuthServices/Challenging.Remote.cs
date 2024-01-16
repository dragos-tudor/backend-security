
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs {

  public static string ChallengeRemoteOAuth<TOptions> (
    HttpContext context,
    TOptions authOptions,
    ISecureDataFormat<AuthenticationProperties> secureDataFormat,
    DateTimeOffset currentUtc)
  where TOptions: OAuthOptions
  {
    var correlationId = GenerateCorrelationId();
    var signinUri = BuildAbsoluteUrl(context.Request, authOptions.CallbackPath);
    var authProperties = new AuthenticationProperties();
    SetupCorrelationCookie(context, authOptions, currentUtc, correlationId);
    SetAuthenticationPropertiesCorrelationId(authProperties, correlationId);
    SetAuthenticationPropertiesRedirectUri(authProperties, GetRequestQueryReturnUrl(context.Request, authOptions.ReturnUrlParameter)!);
    SetAuthenticationPropertiesCallbackUri(authProperties, signinUri);
    if (authOptions.UsePkce) SetAuthenticationPropertiesCodeVerifier(authProperties, GenerateCodeVerifier());

    var state = ProtectAuthenticationProperties(authProperties, secureDataFormat);
    var authParams = CreateAuthorizationParams(authOptions.ClientId, FormatOAuthScopes(authOptions), state, signinUri);
    if (authOptions.UsePkce) AddAuthorizationCodeChallengeParams(authParams, HashCodeVerifier(GetAuthenticationPropertiesCodeVerifier(authProperties)!));

    var authUri = BuildAuthorizationUri(authOptions, authParams);
    LogChallenged(Logger, authOptions.SchemeName, authUri, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, authUri)!;
  }

  public static string ChallengeRemoteOAuth<TOptions> (HttpContext context) where TOptions: OAuthOptions =>
    ChallengeRemoteOAuth(
      context,
      ResolveService<TOptions>(context),
      ResolveService<ISecureDataFormat<AuthenticationProperties>>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

}