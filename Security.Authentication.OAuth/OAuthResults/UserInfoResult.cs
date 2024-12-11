
namespace Security.Authentication.OAuth;

public record class UserInfoResult(IEnumerable<Claim>? Claims, OAuthError? Error = default)
{
  public static implicit operator UserInfoResult(OAuthError error) => new(default, error);

  public static implicit operator UserInfoResult(string error) => new(default, CreateOAuthError(error));

  public void Deconstruct(out IEnumerable<Claim>? claims, out OAuthError? error) { claims = Claims; error = Error; }
}

partial class OAuthFuncs
{
  internal static UserInfoResult CreateUserInfoResult(IEnumerable<Claim>? claims) => new(claims);
}