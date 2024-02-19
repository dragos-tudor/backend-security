using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> SignoutChallengeOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    var signoutMessage = CreateOpenIdConnectMessage();

    SetSignoutChallengeAuthenticationProperties(authProperties, oidcOptions.SignOutRedirectUri, GetRequestUrl(context.Request));
    await SetSignoutChallengeOpenIdConnectMessage(signoutMessage, context, oidcOptions, oidcConfiguration,
      ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    if(IsEmptyString(signoutMessage.IssuerAddress)) return default;

    var redirectUri = await SetSignoutChallengeResponse(context, signoutMessage, oidcOptions, oidcConfiguration);
    LogSignOutChallenge(Logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static Task<string?> SignoutChallengeOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties? authProperties = default)
  where TOptions : OpenIdConnectOptions =>
    SignoutChallengeOidc(
      context,
      authProperties ?? CreateAuthenticationProperties(),
      ResolveService<TOptions>(context),
      ResolveService<OpenIdConnectConfiguration>(context),
      ResolvePropertiesDataFormat<TOptions>(context)
    );
}