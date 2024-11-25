using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> ChallengeSignoutOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    SetSignoutChallengeAuthenticationProperties(authProps, oidcOptions.SignOutRedirectUri, GetRequestUrl(context.Request));
    await SetSignoutChallengeOpenIdConnectMessage(signoutMessage, context, oidcOptions, oidcConfiguration,
      ProtectAuthProps(authProps, propertiesDataFormat));

    if (IsEmptyString(signoutMessage.IssuerAddress)) return default;

    var redirectUri = await SetChallengeSignoutResponse(context, signoutMessage, oidcOptions, oidcConfiguration);
    LogSignOutChallenge(logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }
}