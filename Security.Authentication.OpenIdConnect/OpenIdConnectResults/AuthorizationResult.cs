
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class AuthorizationResult(AuthenticationProperties? AuthProps, OidcData? Authdata, string? Error = default)
{
  public static implicit operator AuthorizationResult(string error) => new(default, default, error);

  public void Deconstruct(out AuthenticationProperties? authProps, OidcData? authdata, out string? error) { authProps = AuthProps; authdata = Authdata; error = Error;  }
}