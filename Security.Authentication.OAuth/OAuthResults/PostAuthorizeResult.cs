
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public record class PostAuthorizeResult(AuthenticationProperties? AuthProps, string? Code, OAuthError? Error = default)
{
  public static implicit operator PostAuthorizeResult(OAuthError error) => new(default, default, error);

  public static implicit operator PostAuthorizeResult(string error) => new(default, default, CreateOAuthError(error));

  public static implicit operator PostAuthorizeResult((AuthenticationProperties? AuthProps, string? Code) result) => new(result.AuthProps, result.Code, default);

  public void Deconstruct(out AuthenticationProperties? authProps, out string? code, out OAuthError? error) { authProps = AuthProps; code = Code; error = Error; }
}