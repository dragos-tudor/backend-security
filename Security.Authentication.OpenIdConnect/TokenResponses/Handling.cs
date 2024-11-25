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
    AuthenticationProperties authProps,
    TOptions oidcOptions,
    StringDataFormat stringDataFormat,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    var tokenError = ValidateTokenResponse(response);
    if (tokenError is not null) return tokenError;

    var responseContent = await ReadHttpResponseContent(response, cancellationToken);
    var tokenMessage = CreateOpenIdConnectMessage(responseContent);

    var securityToken = ToJwtSecurityToken(validationResult.SecurityToken);
    ValidateTokenMessageProtocol(tokenMessage, oidcOptions, securityToken);

    if (ShouldUseTokenLifetime(oidcOptions))
      SetAuthPropsTokenLifetime(authProps, validationResult.SecurityToken!);

    return CreateTokenInfo(tokenMessage, validationResult, securityToken);
  }
}