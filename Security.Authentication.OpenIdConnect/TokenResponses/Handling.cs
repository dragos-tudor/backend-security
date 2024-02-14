using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<TokenResult> HandleTokenResponse<TOptions>(
    HttpResponseMessage response,
    AuthenticationProperties authProperties,
    TOptions oidcOptions,
    OpenIdConnectConfiguration oidcConfiguration,
    StringDataFormat stringDataFormat,
    IRequestCookieCollection cookies,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    var tokenError = ValidateTokenResponse(response);
    if(tokenError is not null) return tokenError;

    var responseContent = await ReadTokenResponseContent(response, cancellationToken);
    var tokenMessage = CreateOpenIdConnectMessage(responseContent);

    var messageError = ValidateTokenMessage(tokenMessage);
    if (messageError is not null) return messageError;

    var validationResult = await UseCodeOrHybridFlowIdToken(tokenMessage.IdToken, oidcOptions, oidcConfiguration);
    if (validationResult.Exception is not null)
      return GetTokenValidationResultError(validationResult);

    if (ShouldUseTokenLifetime(oidcOptions))
      SetAuthenticationPropertiesTokenLifetime(authProperties, validationResult.SecurityToken!);

    var securityToken = ToJwtSecurityToken(validationResult.SecurityToken);
    var tokenNonce = GetSecurityTokenNonce(securityToken);

    ValidatePostAuthorizationMessageProtocol(tokenMessage, oidcOptions, securityToken,
      IsValidNonce(cookies, tokenNonce, oidcOptions, stringDataFormat)? tokenNonce: default);

    return CreateTokenInfo(tokenMessage, validationResult);
  }
}