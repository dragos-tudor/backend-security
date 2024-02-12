using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsTokenValidationResultSuccedded(TokenValidationResult validationResult) =>
    validationResult.Exception is not null;

  static bool IsTokenValidationResultValid(TokenValidationResult validationResult) =>
    validationResult.IsValid;
}