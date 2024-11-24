
namespace Security.Authentication.Cookies;

public sealed record AuthenticationCookieOptions : AuthenticationOptions
{
  public required string CookieName { get; init; }
  public required TimeSpan? ExpireAfter { get; init; }
}
