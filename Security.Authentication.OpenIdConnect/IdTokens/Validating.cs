using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string ValidatedSecurityTokenNotJsonWebToken = "The Validated Security Token must be of type JsonWebToken, but instead its type is '{0}'";
  const string UnableToValidateTokenFromHandler = "Unable to validate the 'id_token', no suitable TokenHandler was found for: '{0}'.";

  static async Task<(SecurityToken?, Exception?)> ValidateIdToken(
    string idToken,
    OpenIdConnectConfiguration oidcConfiguration,
    JsonWebTokenHandler tokenHandler,
    TokenValidationParameters validationParameters)
  {
    string[] issuers = [ oidcConfiguration.Issuer ];
    SetValidationParametersValidIssuers(validationParameters,
      ConcatValidationParametersValidIssuers(validationParameters, issuers) ?? issuers);

    SetValidationParametersIssuerSigningKeys(validationParameters,
      ConcatValidationParametersIssuerSigningKeys(validationParameters, oidcConfiguration.SigningKeys) ?? oidcConfiguration.SigningKeys);

    var validationResult = await tokenHandler.ValidateTokenAsync(idToken, validationParameters);
    if (!IsValidationTokenSuccedded(validationResult))
      return (default, validationResult.Exception);

    var securityToken = validationResult.SecurityToken;
    if (!IsValidSecurityToken(validationResult, securityToken))
      return (default, new (string.Format(UnableToValidateTokenFromHandler, idToken)));

    if (!IsJwtSecurityToken(securityToken))
      return (default, new (string.Format(ValidatedSecurityTokenNotJsonWebToken, securityToken?.GetType())));

    return (securityToken, default);
  }
}