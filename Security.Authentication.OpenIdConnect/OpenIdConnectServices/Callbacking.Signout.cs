using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? CallbackSignoutOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat propertiesDataFormat,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var callbackParams = GetQueryRequestParams(context.Request);
    var callbackMessage = CreateOpenIdConnectMessage(callbackParams);

    var authProperties = UnprotectAuthenticationProperties(callbackMessage.State, propertiesDataFormat);
    var redirectUri = SetResponseRedirect(context.Response, GetCallbackRedirectUri(authProperties!));

    LogSignOutCallback(logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static string? CallbackSignoutOidc<TOptions>(
    HttpContext context,
    ILogger logger)
  where TOptions : OpenIdConnectOptions =>
    CallbackSignoutOidc(
      context,
      ResolveRequiredService<TOptions>(context),
      ResolvePropertiesDataFormat(context),
      logger);
}