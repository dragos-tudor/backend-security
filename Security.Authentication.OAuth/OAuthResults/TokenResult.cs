
namespace Security.Authentication.OAuth;

public record class TokenResult(OAuthTokens? Tokens, OAuthError? Error)
{
  public static implicit operator TokenResult(OAuthTokens tokens) => new(tokens, default);

  public static implicit operator TokenResult(OAuthError error) => new(default, error);

  public static implicit operator TokenResult(string error) => new(default, CreateOAuthError(error));

  public void Deconstruct(out OAuthTokens? tokens, out OAuthError? error) { tokens = Tokens; error = Error; }
}