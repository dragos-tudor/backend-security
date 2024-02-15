
using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

public record TokenInfo(string IdToken, string AccessToken, string TokenType, string RefreshToken,
  ClaimsIdentity Identity, JwtSecurityToken SecurityToken);