
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class TokenResult(OidcTokens? Tokens, JwtSecurityToken? IdToken, string? Error)
{
  public static implicit operator TokenResult(Exception exception) => new(default, default, exception.ToString());

  public static implicit operator TokenResult(string error) => new(default, default, error);

  public void Deconstruct(out OidcTokens? tokens, out JwtSecurityToken? idToken, out string? error) { tokens = Tokens; idToken = IdToken; error = Error;  }
}