using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsSuccessTokenValidationResult(TokenValidationResult validationResult) =>
    validationResult.Exception is not null;

  static bool IsValidTokenValidationResult(TokenValidationResult validationResult) =>
    validationResult.IsValid;
}