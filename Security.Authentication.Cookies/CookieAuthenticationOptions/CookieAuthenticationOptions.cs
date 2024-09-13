
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

public sealed record CookieAuthenticationOptions: AuthenticationOptions
{
  public required TimeSpan ExpireTimeSpan { get; init; }
  public required PathString LogoutPath { get; init; }
  public bool SlidingExpiration { get; init; }
}
