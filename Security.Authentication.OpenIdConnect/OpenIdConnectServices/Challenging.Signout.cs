using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> ChallengeSignoutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var signoutMessage = CreateOpenIdConnectMessage();

    SetSignoutChallengeAuthenticationProperties(authProperties, oidcOptions.SignOutRedirectUri, GetRequestUrl(context.Request));
    await SetSignoutChallengeOpenIdConnectMessage(signoutMessage, context, oidcOptions, oidcConfiguration,
      ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    if(IsEmptyString(signoutMessage.IssuerAddress)) return default;

    var redirectUri = await SetSignoutChallengeResponse(context, signoutMessage, oidcOptions, oidcConfiguration);
    LogSignOutChallenge(logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static Task<string?> ChallengeSignoutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    ChallengeSignoutOidc(
      context,
      authProperties,
      ResolveRequiredService<TOptions>(context),
      ResolveRequiredService<OpenIdConnectConfiguration>(context),
      ResolvePropertiesDataFormat(context),
      logger);
}