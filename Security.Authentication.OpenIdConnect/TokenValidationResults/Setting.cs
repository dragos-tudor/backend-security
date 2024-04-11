using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static TokenValidationResult SetTokenValidationResultException(TokenValidationResult validationResult, string message)
  { validationResult.Exception = new ValidationException(message); return validationResult; }
}