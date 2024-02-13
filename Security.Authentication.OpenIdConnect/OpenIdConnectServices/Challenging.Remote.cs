
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async ValueTask<string?> ChallengeRemoteOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat,
    DateTimeOffset currentUtc)
  where TOptions : OpenIdConnectOptions
  {
    var authMessage = CreateOpenIdConnectMessage();

    UseCorrelationCookie(context, GenerateCorrelationId(), oidcOptions, currentUtc);

    if (ShouldUseCodeChallenge(oidcOptions))
      UseCodeChallenge(authProperties, authMessage.Parameters, GenerateCodeVerifier());

    if (ShouldUseNonce(oidcOptions))
      UseNonce(context, GenerateNonce(), authMessage, oidcOptions, stringDataFormat, currentUtc);

    SetChallengeAuthenticationProperties(authProperties, GetRequestUrl(context.Request), authMessage.RedirectUri, authMessage.State);
    SetChallengeOpenIdConnectMessage(authMessage, context, authProperties, oidcOptions, oidcConfiguration, propertiesDataFormat.Protect(authProperties));

    var authUri = await SetChallengeResponse(context, authMessage, oidcOptions, oidcConfiguration);
    SanitizeChallengeResponse(context.Response);

    LogChallengedRemote(Logger, oidcOptions.SchemeName, authUri!, context.TraceIdentifier);
    return authUri;
  }

  public static ValueTask<string?> ChallengeRemoteOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties)
  where TOptions : OpenIdConnectOptions =>
      ChallengeRemoteOidc(
        context,
        authProperties,
        ResolveService<TOptions>(context),
        ResolveService<OpenIdConnectConfiguration>(context),
        ResolveService<PropertiesDataFormat>(context),
        ResolveService<StringDataFormat>(context),
        ResolveService<TimeProvider>(context).GetUtcNow()
      );
}