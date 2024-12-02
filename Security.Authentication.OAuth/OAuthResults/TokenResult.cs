
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public record class TokenResult(OAuthTokens? Tokens, string? Error)
{
  public static implicit operator TokenResult(OAuthTokens tokens) => new(tokens, default);

  public static implicit operator TokenResult(string error) => new(default, error);

  public void Deconstruct(out OAuthTokens? tokens, out string? error) { tokens = Tokens; error = Error;  }
}