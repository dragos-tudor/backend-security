
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static async Task<TokenValidationResult> ValidateIdToken<TOptions>(
    string idToken,
    TOptions oidcOptions)
  where TOptions : OpenIdConnectOptions
  {
    var validationParameters = CreateTokenValidationParameters(oidcOptions);
    SetIdTokenValidationParameters(validationParameters, oidcOptions);
    SetRequireSignedTokensValidationParameters(validationParameters, false);

    var tokenHandler = new JwtSecurityTokenHandler();
    var validationResult = await tokenHandler.ValidateTokenAsync(idToken, validationParameters);
    if (!IsSuccessTokenValidationResult(validationResult))
      return validationResult;

    if (!IsValidTokenValidationResult(validationResult))
      return SetTokenValidationResultException(validationResult, $"Id token is invalid: '{idToken}'.");

    var securityToken = validationResult.SecurityToken;
    if (!IsJsonWebToken(securityToken))
      return SetTokenValidationResultException(validationResult, $"Validated security token should be JsonWebToken, but is '{securityToken?.GetType()}");

    return validationResult;
  }
}