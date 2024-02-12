using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static IEnumerable<string>? ConcatValidationParametersValidIssuers(
    TokenValidationParameters validationParameters,
    IEnumerable<string> issuers) =>
      validationParameters.ValidIssuers?.Concat(issuers);

  static IEnumerable<SecurityKey>? ConcatValidationParametersIssuerSigningKeys(
    TokenValidationParameters validationParameters,
    IEnumerable<SecurityKey> signingKeys) =>
      validationParameters.IssuerSigningKeys?.Concat(signingKeys);
}