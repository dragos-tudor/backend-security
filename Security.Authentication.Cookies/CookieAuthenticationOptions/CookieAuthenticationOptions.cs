
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

public record CookieAuthenticationOptions: AuthenticationOptions
{
  public PathString AccessDeniedPath { get; init; } = "/accessdenied";
  public TimeSpan? ExpireTimeSpan { get; init; }
  public PathString LoginPath { get; init; } = "/login";
  public PathString LogoutPath { get; init; } = "/logout";
  public string ReturnUrlParameter { get; init; } = "returnUrl";
  public bool SlidingExpiration { get; init; }
}
