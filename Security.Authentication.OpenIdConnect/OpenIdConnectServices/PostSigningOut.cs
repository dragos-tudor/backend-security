

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static PostSignOutResult PostSignOut(
    HttpContext context,
    PropertiesDataFormat authPropsProtector)
  {
    if (IsOAuthError(context.Request)) return GetOAuthError(context.Request);

    var signoutResponse = GetHttpRequestQueryParams(context.Request);
    var signoutData = ToOpenIdConnectData(signoutResponse);

    var state = GetOidcDataState(signoutData);
    if (state is null) return CreateAuthProps();

    return UnprotectAuthProps(state!, authPropsProtector) ?? CreateAuthProps();
  }
}