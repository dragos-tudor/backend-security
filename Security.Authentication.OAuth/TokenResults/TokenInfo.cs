
namespace Security.Authentication.OAuth;

public sealed record TokenInfo {

  public string? AccessToken { get; init; }
  public string? TokenType { get; init; }
  public string? RefreshToken { get; init; }
  public string? ExpiresIn { get; init; }

}