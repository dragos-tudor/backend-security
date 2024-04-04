using System.Collections.Generic;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static IEnumerable<string> SetValidationParametersValidIssuers(
    TokenValidationParameters validationParameters,
    IEnumerable<string> issuers) =>
      validationParameters.ValidIssuers = issuers;

  static IEnumerable<SecurityKey> SetValidationParametersIssuerSigningKeys(
    TokenValidationParameters validationParameters,
    IEnumerable<SecurityKey> signingKeys) =>
      validationParameters.IssuerSigningKeys = signingKeys;

  static bool SetValidationParametersRequireSignedTokens(
    TokenValidationParameters validationParameters,
    bool requredSignedTokens) =>
      validationParameters.RequireSignedTokens = requredSignedTokens;

  static void SetValidationParametersForIdTokenValidation(
    TokenValidationParameters validationParameters,
    OpenIdConnectConfiguration oidcConfiguration)
  {
    string[] issuers = [oidcConfiguration.Issuer];
    SetValidationParametersValidIssuers(validationParameters,
      ConcatValidationParametersValidIssuers(validationParameters, issuers) ?? issuers);

    SetValidationParametersIssuerSigningKeys(validationParameters,
      ConcatValidationParametersIssuerSigningKeys(validationParameters, oidcConfiguration.SigningKeys) ?? oidcConfiguration.SigningKeys);
  }

}