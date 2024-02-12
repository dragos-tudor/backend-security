using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static async Task<TokenValidationResult> ValidateIdToken(
    string idToken,
    JsonWebTokenHandler tokenHandler,
    TokenValidationParameters validationParameters)
  {
    var validationResult = await tokenHandler.ValidateTokenAsync(idToken, validationParameters);
    if (!IsTokenValidationResultSuccedded(validationResult))
      return validationResult;

    if (!IsTokenValidationResultValid(validationResult))
      return SetTokenValidationResultException(validationResult, $"Id token is invalid: '{idToken}'.");

    var securityToken = validationResult.SecurityToken;
    if (!IsJsonWebToken(securityToken))
      return SetTokenValidationResultException(validationResult, $"Validated security token should be JsonWebToken, but is '{securityToken?.GetType()}");

    return validationResult;
  }
}