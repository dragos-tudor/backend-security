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
    OpenIdConnectValidationOptions oidcValidationOptions,
    string code,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    var tokenError = ValidateTokenResponse(response);
    if (tokenError is not null) return tokenError;

    var tokenResponse = await ReadHttpResponseContent(response, cancellationToken);
    var tokenData = ToOpenIdConnectData(tokenResponse);
    var rawIdToken = GetOidcDataIdToken(tokenData);
    var accessToken = GetOidcDataAccessToken(tokenData);

    var securityResult = await oidcOptions.TokenHandler.ValidateTokenAsync(rawIdToken, oidcOptions.TokenValidationParameters);
    if (securityResult.Exception is not null) return securityResult.Exception;

    var idToken = ToJwtSecurityToken(securityResult.SecurityToken);
    var validationError = ValidateTokenResponse(oidcValidationOptions, idToken, accessToken!, oidcOptions.ClientId, code);
    if (validationError is not null) return validationError;

    if (ShouldUseTokenLifetime(oidcOptions)) SetAuthPropsTokenLifetime(authProps, idToken!);

    var identity = securityResult.ClaimsIdentity;
    return new (CreateOidcTokens(tokenData), idToken, default);
  }
}