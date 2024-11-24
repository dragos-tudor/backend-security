
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components.Forms;

namespace Security.Authentication.OAuth;

public record class UserInfoResult(Claim[]? Claims, string? Error = default)
{
  public static implicit operator UserInfoResult(string error) => new(default, error);

  public static implicit operator UserInfoResult(Claim[] claims) => new(claims);

  public void Deconstruct(out Claim[]? claims, out string? error) { claims = Claims; error = Error;  }
}