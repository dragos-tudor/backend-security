
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OpenIdConnect;

public record class UserInfoResult(Claim[]? Claims, string? Error)
{
  public static implicit operator UserInfoResult(string error) => new(default, error);

  public static implicit operator UserInfoResult(Claim[]? claims) => new(claims, default);

  public void Deconstruct(out Claim[]? claims, out string? error) { claims = Claims; error = Error;  }
}