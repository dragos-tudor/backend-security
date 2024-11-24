
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public record class AuthorizationResult(AuthenticationProperties? AuthProps, string? Error)
{
  public static implicit operator AuthorizationResult(string error) => new(default, error);

  public static implicit operator AuthorizationResult(AuthenticationProperties authProps) => new(authProps, default);

  public void Deconstruct(out AuthenticationProperties? authProps, out string? error) { authProps = AuthProps; error = Error;  }
}