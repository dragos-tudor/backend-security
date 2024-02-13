
namespace Security.Authentication.OAuth;

public record class TokenResult (TokenInfo? Success, string? Failure);