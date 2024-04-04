using System.Text.Json;

namespace Security.Authentication.OAuth;

partial record class TokenResult
{
  public static implicit operator TokenResult(string failure) =>
    CreateFailureTokenResult(failure);

  public static implicit operator TokenResult(JsonElement json) =>
    CreateSuccessTokenResult(json);
}