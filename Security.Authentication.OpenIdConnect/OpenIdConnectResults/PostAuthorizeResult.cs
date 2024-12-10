
namespace Security.Authentication.OpenIdConnect;

public record class PostAuthorizeResult(AuthenticationProperties? AuthProps, string? Code, OAuthError? Error = default)
{
  public static implicit operator PostAuthorizeResult(OAuthError error) => new(default, default, error);

  public static implicit operator PostAuthorizeResult(string error) => new(default, default, CreateOAuthError(error));

  public void Deconstruct(out AuthenticationProperties? authProps, string? code, out OAuthError? error) { authProps = AuthProps; code = Code; error = Error;  }
}