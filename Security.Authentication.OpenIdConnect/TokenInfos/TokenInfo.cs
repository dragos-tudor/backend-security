
namespace Security.Authentication.OpenIdConnect;

public record TokenInfo(string? IdToken, string? AccessToken, string? TokenType = default, string? RefreshToken = default,
  string? ExpiresIn = default, ClaimsIdentity? Identity = default);