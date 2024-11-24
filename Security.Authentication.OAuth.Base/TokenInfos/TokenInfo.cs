
namespace Security.Authentication.OAuth;

public record class TokenInfo(string? AccessToken, string? RefreshToken = default, string? TokenType = default, string? ExpiresIn = default);