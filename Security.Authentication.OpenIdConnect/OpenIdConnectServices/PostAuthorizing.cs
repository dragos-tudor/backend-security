
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string InvalidPostAuthorizationMessageState = "Invalid oidc authorization message state.";
  const string NoPostAuthorizationMessage = "No oidc authorization message.";
  const string NoPostAuthorizationMessageState = "No oidc authorization message state.";
  const string UnexpectedPostAuthorizationTokens = "Oidc authorization message cannot contain an identity token or an access token when using query response mode";

  public static async Task<PostAuthorizationResult> PostAuthorization<TOptions>(
    HttpContext context,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    PropertiesDataFormat propertiesDataFormat,
    StringDataFormat stringDataFormat)
  where TOptions : OpenIdConnectOptions
  {
    var postAuthParams = await GetRequestParams(context.Request, context.RequestAborted);
    if (postAuthParams is null) return NoPostAuthorizationMessage;

    var postAuthMessage = CreateOpenIdConnectMessage(postAuthParams);
    if (IsGetRequest(context.Request) && !IsSafePostAuthorizationMessage(postAuthMessage)) return UnexpectedPostAuthorizationTokens;
    if (IsEmptyString(postAuthMessage.State)) return NoPostAuthorizationMessageState;

    var postAuthError = ValidatePostAuthorizationMessage(postAuthMessage);
    if (postAuthError is not null) return postAuthError;

    var authProperties = UnprotectAuthenticationProperties(postAuthMessage.State, propertiesDataFormat);
    if (authProperties is null) return InvalidPostAuthorizationMessageState;

    var correlationError = ValidateCorrelationCookie(context.Request, authProperties);
    if (correlationError is not null) return correlationError;

    SetPostAuthorizationOpenIdConnectMessage(postAuthMessage, authProperties);
    SetPostAuthorizationAuthenticationProperties(authProperties, postAuthMessage, oidcOptions);

    if (!IsOpenIdConnectImplicitOrHybridFlow(postAuthMessage))
    {
      ValidatePostAuthorizationMessageProtocol(postAuthMessage, oidcOptions);
      return CreatePostAuthorizationInfo(authProperties);
    }

    var validationResult = await UseImplicitOrHybridFlowIdToken(postAuthMessage.IdToken, oidcOptions, oidcConfiguration);
    if (validationResult.Exception is not null)
      return GetTokenValidationResultError(validationResult);

    var securityToken = ToJwtSecurityToken(validationResult.SecurityToken);
    var tokenNonce = GetSecurityTokenNonce(securityToken);
    var validNonce = IsValidNonce(GetRequestCookies(context.Request), tokenNonce, oidcOptions, stringDataFormat)? tokenNonce: default;
    ValidatePostAuthorizationMessageProtocol(postAuthMessage, oidcOptions, securityToken, validNonce);

    if (ShouldUseTokenLifetime(oidcOptions))
      SetAuthenticationPropertiesTokenLifetime(authProperties, validationResult.SecurityToken!);

    return CreatePostAuthorizationInfo(authProperties, postAuthMessage, validationResult, securityToken);
  }

  public static Task<PostAuthorizationResult> PostAuthorization<TOptions>(
    HttpContext context)
  where TOptions : OpenIdConnectOptions =>
      PostAuthorization(
        context,
        ResolveRequiredService<TOptions>(context),
        ResolveRequiredService<OpenIdConnectConfiguration>(context),
        ResolvePropertiesDataFormat<TOptions>(context),
        ResolveStringDataFormat<TOptions>(context)
      );
}