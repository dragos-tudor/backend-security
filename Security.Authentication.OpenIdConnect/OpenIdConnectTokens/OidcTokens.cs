
using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

public record class OidcTokens(string? IdToken, string? AccessToken, string? RefreshToken = default, string? TokenType = default, string? ExpiresIn = default): OAuthTokens(AccessToken, RefreshToken, TokenType, ExpiresIn);