using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string? SignOutCallbackOidc<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat propertiesDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    var callbackParams = GetQueryRequestParams(context.Request);
    var callbackMessage = CreateOpenIdConnectMessage(callbackParams);

    var authProperties = UnprotectAuthenticationProperties(callbackMessage.State, propertiesDataFormat);
    var redirectUri = SetResponseRedirect(context.Response, GetCallbackRedirectUri(authProperties));

    LogSignOutCallback(ResolveOpenIdConnectLogger(context), oidcOptions.SchemeName, redirectUri!, context.TraceIdentifier);
    return redirectUri;
  }

  public static string? SignOutCallbackOidc<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
    SignOutCallbackOidc(
      context,
      ResolveService<TOptions>(context),
      ResolvePropertiesDataFormat<TOptions>(context)
    );
}