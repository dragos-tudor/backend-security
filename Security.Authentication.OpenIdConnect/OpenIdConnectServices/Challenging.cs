
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async ValueTask<string?> ChallengeOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat,
    DateTimeOffset currentUtc)
  where TOptions : OpenIdConnectOptions
  {
    var challengeMessage = CreateOpenIdConnectMessage();
    UseCorrelationCookie(context, GenerateCorrelationId(), oidcOptions, currentUtc);

    if (ShouldUseCodeChallenge(oidcOptions))
      UseCodeChallenge(authProperties, challengeMessage.Parameters, GenerateCodeVerifier());

    if (ShouldUseNonce(oidcOptions))
      UseNonce(context, GenerateNonce(), challengeMessage, oidcOptions, stringDataFormat, currentUtc);

    SetAuthorizationAuthenticationProperties(authProperties, GetRequestUrl(context.Request), challengeMessage.RedirectUri, challengeMessage.State);
    SetAuthorizationOpenIdConnectMessage(challengeMessage, context, authProperties, oidcOptions, oidcConfiguration,
      ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    var authUri = await SetAuthorizationResponse(context, challengeMessage, oidcOptions, oidcConfiguration);
    SanitizeResponse(context.Response);

    LogAuthorizeChallenge(ResolveOpenIdConnectLogger(context), oidcOptions.SchemeName, authUri!, context.TraceIdentifier);
    return authUri;
  }

  public static ValueTask<string?> ChallengeOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions : OpenIdConnectOptions =>
      ChallengeOidc(
        context,
        authProperties,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectConfiguration>(context),
        ResolvePropertiesDataFormat<TOptions>(context),
        ResolveStringDataFormat<TOptions>(context),
        ResolveTimeProvider<TOptions>(context).GetUtcNow()
      );
}