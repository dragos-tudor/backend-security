using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static PostSignOutResult PostSignOut<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions : OpenIdConnectOptions
  {
    if (IsOAuthError(context.Request)) return GetOAuthErrorType(context.Request);

    var signoutResponse = GetHttpRequestQueryParams(context.Request);
    var signoutData = ToOpenIdConnectData(signoutResponse);

    var state = GetOidcDataState(signoutData);
    if (state is null) return CreateAuthProps();

    return UnprotectAuthProps(state!, authPropsProtector) ?? CreateAuthProps();
  }
}