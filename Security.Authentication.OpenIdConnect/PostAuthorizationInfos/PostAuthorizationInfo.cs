using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class PostAuthorizationInfo(AuthenticationProperties AuthProperties, string? Code, string? IdToken,
  ClaimsIdentity? Identity, JwtSecurityToken? SecurityToken);