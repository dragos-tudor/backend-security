using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<PostSignOutResult> PostSignOut<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions : OpenIdConnectOptions
  {
    if (IsOAuthError(context.Request)) return GetOAuthErrorType(context.Request);

    var signoutResponse = await GetHttpRequestParams(context.Request, context.RequestAborted);
    if (signoutResponse is null) return InvalidAuthorizationResponse;

    var signoutData = ToOpenIdConnectData(signoutResponse);
    var state = GetOidcDataState(signoutData);
    if (state is null) return InvalidState;

    var authProps = UnprotectAuthProps(state!, authPropsProtector);
    if (authProps is null) return UnprotectStateFailed;

    var principal = GetContextUser(context);
    var validationError = ValidateSignoutResponse(signoutData, principal);
    if (validationError is not null) return validationError;

    return authProps;
  }
}