
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class PostAuthorizeResult(AuthenticationProperties? AuthProps, string? Code, string? Error = default)
{
  public static implicit operator PostAuthorizeResult(string error) => new(default, default, error);

  public void Deconstruct(out AuthenticationProperties? authProps, string? code, out string? error) { authProps = AuthProps; code = Code; error = Error;  }
}