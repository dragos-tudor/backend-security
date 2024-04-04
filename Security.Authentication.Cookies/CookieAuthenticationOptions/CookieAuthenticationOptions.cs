
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

public sealed record CookieAuthenticationOptions: AuthenticationOptions
{
  public required PathString AccessDeniedPath { get; init; }
  public required TimeSpan ExpireTimeSpan { get; init; }
  public required PathString LoginPath { get; init; }
  public required PathString LogoutPath { get; init; }
  public required string ReturnUrlParameter { get; init; }
  public bool SlidingExpiration { get; init; }
}
