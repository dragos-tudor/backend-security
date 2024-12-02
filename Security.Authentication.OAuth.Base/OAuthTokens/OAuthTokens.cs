
namespace Security.Authentication.OAuth;

public record class OAuthTokens(string? AccessToken, string? RefreshToken = default, string? TokenType = default, string? ExpiresIn = default);