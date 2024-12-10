using System.Net.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<TokenResult> HandleTokenResponse<TOptions>(
    HttpResponseMessage response,
    TOptions oidcOptions,
    OpenIdConnectValidationOptions oidcValidationOptions,
    string code,
    CancellationToken cancellationToken = default)
  where TOptions : OpenIdConnectOptions
  {
    if (!IsSuccessHttpResponse(response)) return await ReadJsonOAuthError(response, cancellationToken);

    var tokenError = ValidateTokenResponse(response);
    if (tokenError is not null) return tokenError;

    var oidcTokens = await ReadTokenOidcTokens(response, cancellationToken);
    var oidcTokensError = ValidateOidcTokens(oidcTokens);
    if (oidcTokensError is not null) return oidcTokensError;

    var secValidation = await ValidateSecurityIdToken(oidcOptions, oidcTokens.IdToken);
    if (secValidation.Exception is not null) return secValidation.Exception;

    var idToken = ToJwtSecurityToken(secValidation.SecurityToken);
    var validationError = ValidateIdToken(oidcValidationOptions, idToken, oidcTokens.AccessToken!, oidcOptions.ClientId, code);
    if (validationError is not null) return validationError;

    return new(oidcTokens, idToken, default);
  }
}