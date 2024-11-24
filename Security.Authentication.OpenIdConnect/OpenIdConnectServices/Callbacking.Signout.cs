using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? CallbackSignoutOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector,
    ILogger logger)
  where TOptions : OpenIdConnectOptions
  {
    var callbackParams = GetHttpRequestQueryParams(context.Request);
    var callbackMessage = CreateOpenIdConnectMessage(callbackParams);

    var authProps = UnprotectAuthProps(callbackMessage.State, authPropsProtector);
    var redirectUri = SetHttpResponseRedirect(context.Response, GetAuthPropsRedirectUri(authProps)!);

    LogSignOutCallback(logger, oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }
}