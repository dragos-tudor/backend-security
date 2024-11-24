
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public record class TokenResult(TokenInfo? TokenInfo, string? Error)
{
  public static implicit operator TokenResult(string error) => new(default, error);

  public static implicit operator TokenResult(TokenInfo tokenInfo) => new(tokenInfo, default);

  public void Deconstruct(out TokenInfo? tokenInfo, out string? error) { tokenInfo = TokenInfo; error = Error;  }
}