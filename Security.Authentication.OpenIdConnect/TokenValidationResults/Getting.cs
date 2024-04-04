using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GetTokenValidationResultError(TokenValidationResult validationResult) =>
    validationResult.Exception.Message;
}