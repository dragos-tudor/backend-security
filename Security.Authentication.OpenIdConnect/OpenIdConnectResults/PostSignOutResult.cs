
namespace Security.Authentication.OpenIdConnect;

public record class PostSignOutResult(AuthenticationProperties? AuthProps, OAuthError? Error = default)
{
  public static implicit operator PostSignOutResult(AuthenticationProperties authProps) => new(authProps, default);

  public static implicit operator PostSignOutResult(OAuthError error) => new(default, error);

  public static implicit operator PostSignOutResult(string error) => new(default, CreateOAuthError(error));

  public void Deconstruct(out AuthenticationProperties? authProps, out OAuthError? error) { authProps = AuthProps; error = Error;  }
}