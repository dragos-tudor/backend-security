
namespace Security.Authentication.OAuth;

public record class TokenInfo(string? AccessToken, string? TokenType = default, string? RefreshToken = default, string? ExpiresIn = default);