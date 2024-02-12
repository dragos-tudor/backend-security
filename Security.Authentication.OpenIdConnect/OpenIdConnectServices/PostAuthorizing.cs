
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string InvalidAuthorizationMessageState = "Invalid oidc authorization message state.";
  const string NoAuthorizationMessage = "No oidc authorization message.";
  const string NoAuthorizationMessageState = "No oidc authorization message state.";
  const string UnexpectedAuthorizationTokens = "Oidc authorization message cannot contain an identity token or an access token when using query response mode";

  public static async Task<PostAuthorizeResult> PostAuthorize<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    OpenIdConnectProtocolValidator protocolValidator,
    JsonWebTokenHandler tokenHandler,
    NonceCookieBuilder cookieBuilder,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    var authParams = await GetPostAuthorizeParams(context.Request, context.RequestAborted);
    if (authParams is null) return NoAuthorizationMessage;

    var authMessage = CreateOpenIdConnectMessage(authParams);
    if (IsGetRequest(context.Request) && !IsSafePostAuthorizeResponse(authMessage)) return UnexpectedAuthorizationTokens;
    if (IsEmptyString(authMessage.State)) return NoAuthorizationMessageState;

    var authError = ValidatePostAuthorizeResponse(authMessage);
    if (authError is not null) return authError;

    var authProperties = UnprotectAuthenticationProperties(authMessage.State, propertiesDataFormat);
    if (authProperties is null) return InvalidAuthorizationMessageState;

    var correlationError = ValidateCorrelationCookie(context.Request, authProperties);
    if (correlationError is not null) return correlationError;

    SetPostAuthorizeOpenIdConnectMessage(authMessage, authProperties);
    SetPostAuthorizeAuthenticationProperties(authProperties, authMessage, oidcOptions);

    if (!IsOpenIdConnectImplicitOrHybridFlow(authMessage))
    {
      ValidatePostAuthorizeResponseProtocol(authMessage, oidcOptions, protocolValidator);
      return CreatePostAuthorizeSuccess(authProperties);
    }

    var validationResult = await UseImplicitOrHybridFlowIdToken(authMessage.IdToken, authProperties, oidcOptions, oidcConfiguration, tokenHandler);
    if (validationResult.Exception is not null)
      return validationResult.Exception.Message;

    var securityToken = ToJwtSecurityToken(validationResult.SecurityToken);
    var tokenNonce = GetSecurityTokenNonce(securityToken);
    ValidatePostAuthorizeResponseProtocol(authMessage, oidcOptions, protocolValidator, securityToken,
      IsValidNonce(context.Request, tokenNonce, cookieBuilder, oidcOptions, stringDataFormat)? tokenNonce: default);

    return CreatePostAuthorizeSuccess(authProperties, authMessage.Code, authMessage.IdToken, validationResult.ClaimsIdentity);
  }

  public static Task<PostAuthorizeResult> PostAuthorize<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
      PostAuthorize(
        context,
        ResolveService<TOptions>(context),
        ResolveService<OpenIdConnectConfiguration>(context),
        ResolveService<OpenIdConnectProtocolValidator>(context),
        ResolveService<JsonWebTokenHandler>(context),
        ResolveService<NonceCookieBuilder>(context),
        ResolveService<PropertiesDataFormat>(context),
        ResolveService<StringDataFormat>(context)
      );
}