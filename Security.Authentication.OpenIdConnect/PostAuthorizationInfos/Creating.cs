using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static PostAuthorizationInfo CreatePostAuthorizationInfo(
    AuthenticationProperties authProperties,
    OpenIdConnectMessage? oidcMessage = default,
    TokenValidationResult? validationResult = default,
    JwtSecurityToken? securityToken = default) =>
      new (authProperties, oidcMessage?.Code, oidcMessage?.IdToken, validationResult?.ClaimsIdentity, securityToken);
}