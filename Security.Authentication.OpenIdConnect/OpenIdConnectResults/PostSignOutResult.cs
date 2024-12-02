
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class PostSignOutResult(AuthenticationProperties? AuthProps, string? Error = default)
{
  public static implicit operator PostSignOutResult(AuthenticationProperties authProps) => new(authProps, default);

  public static implicit operator PostSignOutResult(string error) => new(default, error);

  public void Deconstruct(out AuthenticationProperties? authProps, out string? error) { authProps = AuthProps; error = Error;  }
}