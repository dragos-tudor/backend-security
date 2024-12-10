
using System.IdentityModel.Tokens.Jwt;

namespace Security.Authentication.OpenIdConnect;

public record class TokenResult(OidcTokens? Tokens, JwtSecurityToken? IdToken, OAuthError? Error)
{
  public static implicit operator TokenResult(Exception exception) => new(default, default, CreateOAuthError(exception.ToString()));

  public static implicit operator TokenResult(OAuthError error) => new(default, default, error);

  public static implicit operator TokenResult(string error) => new(default, default, CreateOAuthError(error));

  public void Deconstruct(out OidcTokens? tokens, out JwtSecurityToken? idToken, out OAuthError? error) { tokens = Tokens; idToken = IdToken; error = Error;  }
}