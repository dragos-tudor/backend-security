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

}