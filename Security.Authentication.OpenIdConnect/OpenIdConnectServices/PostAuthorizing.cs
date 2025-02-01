
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string InvalidAuthorizationResponse = "invaliud oidc authorization response [no params]";
  internal const string UnprotectAuthorizationStateFailed = "unprotect oidc authorization state failed";

  public static async Task<PostAuthorizeResult> PostAuthorize<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions : OpenIdConnectOptions
  {
    if (IsOAuthError(context.Request)) return GetOAuthError(context.Request);

    var authResponse = await GetHttpRequestParams(context.Request, context.RequestAborted);
    if (authResponse is null) return InvalidAuthorizationResponse;

    var authData = ToOpenIdConnectData(authResponse);
    var code = GetOidcDataAuthorizationCode(authData);
    var state = GetOidcDataState(authData);
    var validationError = ValidateCallbackResponse(validationOptions, code, state);
    if (validationError is not null) return validationError;

    var authProps = UnprotectAuthProps(state!, authPropsProtector);
    if (authProps is null) return UnprotectAuthorizationStateFailed;

    var correlationError = ValidateCorrelationCookie(context.Request, authProps);
    if (correlationError is not null) return correlationError;

    var userState = GetAuthPropsUserState(authProps);
    SetAuthPropsSession(authProps, oidcOptions, userState);

    return new(authProps, code);
  }
}