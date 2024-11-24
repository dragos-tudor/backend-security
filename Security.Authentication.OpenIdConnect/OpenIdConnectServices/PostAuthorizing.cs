
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  internal const string AuthorizationCodeNotFound = "oidc authorization code was not found";
  internal const string InvalidState = "oidc state was missing or invalid";
  internal const string InvalidAuthorizationResponse = "invaliud oidc authorization response";
  internal const string UnprotectStateFailed = "unprotect oidc state failed";

  public static async Task<AuthorizationResult> PostAuthorization<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    PropertiesDataFormat authPropsProtector,
    StringDataFormat stringDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    if(IsOAuthError(context.Request)) return GetOAuthErrorType(context.Request);

    var authResponse = await GetHttpRequestParams(context.Request, context.RequestAborted);
    if(authResponse is null) return InvalidAuthorizationResponse;

    var authData = ToOpenIdConnectData(authResponse);
    var state = GetOidcDataState(authData);
    if(state is null) return InvalidState;

    var authProps = UnprotectAuthProps(state!, authPropsProtector);
    if(authProps is null) return UnprotectStateFailed;

    var correlationError = ValidateCorrelationCookie(context.Request, authProps);
    if(correlationError is not null) return correlationError;

    var correlationId = GetAuthPropsCorrelationId(authProps);
    DeleteCorrelationCookie(context, oidcOptions, correlationId);
    UnsetAuthPropsCorrelationId(authProps);

    return new (authProps, authData);
  }
}