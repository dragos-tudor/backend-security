
namespace Security.Authentication.OAuth;

public sealed record OAuthError(string ErrorType, string? ErrorDescription = default, string? ErrorUri = default);