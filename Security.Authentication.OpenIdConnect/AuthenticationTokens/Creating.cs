using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static AuthenticationToken CreateAuthenticationToken(string name, string value) =>
    new () { Name = name, Value = value };

  static AuthenticationToken CreateAuthenticationAccessToken(string accessToken) =>
    CreateAuthenticationToken(OpenIdConnectParameterNames.AccessToken, accessToken);

  static AuthenticationToken CreateAuthenticationIdToken(string idToken) =>
    CreateAuthenticationToken(OpenIdConnectParameterNames.IdToken, idToken);

  static AuthenticationToken CreateAuthenticationRefreshToken(string refreshToken) =>
    CreateAuthenticationToken(OpenIdConnectParameterNames.RefreshToken, refreshToken);

  static AuthenticationToken CreateAuthenticationTokenType(string tokenType) =>
    CreateAuthenticationToken(OpenIdConnectParameterNames.TokenType, tokenType);

  static AuthenticationToken CreateAuthenticationTokenExpiresAt(string expiresAt) =>
    CreateAuthenticationToken("expires_at", expiresAt);
}