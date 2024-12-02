
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public record class PostAuthorizeResult(AuthenticationProperties? AuthProps, string? Code, string? Error = default)
{
  public static implicit operator PostAuthorizeResult(string error) => new(default, default, error);

  public void Deconstruct(out AuthenticationProperties? authProps, out string? code, out string? error) { authProps = AuthProps; code = Code; error = Error; }
}