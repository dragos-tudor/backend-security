
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

public record CookieAuthenticationOptions: AuthenticationOptions {

  public PathString AccessDeniedPath { get; init; } = default!;
  public TimeSpan ExpireTimeSpan { get; init; }
  public PathString LoginPath { get; init; } = default!;
  public PathString LogoutPath { get; init; } = default!;
  public string ReturnUrlParameter { get; init; } = default!;
  public bool SlidingExpiration { get; init; }

  public ICookieManager CookieManager { get; init; } = default!;
  public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; init; } = default!;

}
