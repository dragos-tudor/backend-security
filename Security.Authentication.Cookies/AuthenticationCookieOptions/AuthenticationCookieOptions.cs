
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

public sealed record AuthenticationCookieOptions: AuthenticationOptions
{
  public required string CookieName { get; set; }
  public required TimeSpan ExpireTimeSpan { get; init; }
  public required PathString LogoutPath { get; init; }
  public bool SlidingExpiration { get; init; }
}
