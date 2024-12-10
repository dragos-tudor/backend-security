using System.Net.Http;

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

    var secValidation = await ValidateSecurityIdToken(oidcOptions, rawIdToken);
    if (secValidation.Exception is not null) return secValidation.Exception;

    var idToken = ToJwtSecurityToken(secValidation.SecurityToken);
    var accessToken = GetOidcDataAccessToken(tokenData);
    var validationError = ValidateIdToken(oidcValidationOptions, idToken, accessToken!, oidcOptions.ClientId, code);
    if (validationError is not null) return validationError;

    return new(CreateOidcTokens(tokenData), idToken, default);
  }

}