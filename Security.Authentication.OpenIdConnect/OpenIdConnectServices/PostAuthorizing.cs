
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string AuthorizationCodeNotFound = "oidc authorization code was not found";
  internal const string InvalidAuthorizationResponse = "invaliud oidc authorization response [no params]";
  internal const string InvalidAuthorizationState = "oidc authorization state was missing or invalid";
  internal const string UnprotectAuthorizationStateFailed = "unprotect oidc authorization state failed";

  public static async Task<PostAuthorizeResult> PostAuthorize<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions validationOptions,
    PropertiesDataFormat authPropsProtector)
  where TOptions : OpenIdConnectOptions
  {
    if (IsOAuthError(context.Request)) return GetOAuthErrorType(context.Request);

    var authResponse = await GetHttpRequestParams(context.Request, context.RequestAborted);
    if (authResponse is null) return InvalidAuthorizationResponse;

    var authData = ToOpenIdConnectData(authResponse);
    var state = GetOidcDataState(authData);
    if (state is null) return InvalidAuthorizationState;

    var authProps = UnprotectAuthProps(state!, authPropsProtector);
    if (authProps is null) return UnprotectAuthorizationStateFailed;

    var code = GetOidcDataAuthorizationCode(authData);
    var validationError = ValidateAuthenticationResponse(validationOptions, code, state);
    if (validationError is not null) return validationError;

    var correlationError = ValidateCorrelationCookie(context.Request, authProps);
    if (correlationError is not null) return correlationError;

    var correlationId = GetAuthPropsCorrelationId(authProps);
    DeleteCorrelationCookie(context, oidcOptions, correlationId);
    RemoveAuthPropsCorrelationId(authProps);

    var userState = GetAuthPropsUserState(authProps);
    SetOidcDataState(authData, userState!);
    SetAuthPropsSession(authProps, oidcOptions, authData);

    return new (authProps, code);
  }
}