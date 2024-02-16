using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<string?> SignoutRemoteOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    var signoutMessage = CreateOpenIdConnectMessage();

    SetSignoutRemoteAuthenticationProperties(authProperties, oidcOptions.SignedOutRedirectUri, GetRequestUrl(context.Request));
    await SetSignoutRemoteOpenIdConnectMessage(signoutMessage, context, oidcOptions, oidcConfiguration,
      ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    if(IsEmptyString(signoutMessage.IssuerAddress)) return default;

    var redirectUri = await SetSignoutRemoteResponse(context, signoutMessage, oidcOptions, oidcConfiguration);
    LogSignOutRemote(Logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static Task<string?> SignoutRemoteOidc<TOptions>(
    HttpContext context,
    AuthenticationProperties? authProperties = default)
  where TOptions : OpenIdConnectOptions =>
    SignoutRemoteOidc(
      context,
      authProperties ?? CreateAuthenticationProperties(),
      ResolveService<TOptions>(context),
      ResolveService<OpenIdConnectConfiguration>(context),
      ResolvePropertiesDataFormat<TOptions>(context)
    );
}