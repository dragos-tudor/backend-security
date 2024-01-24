using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static bool IsValidationTokenSuccedded(TokenValidationResult validationResult) =>
    validationResult.Exception is not null;
}