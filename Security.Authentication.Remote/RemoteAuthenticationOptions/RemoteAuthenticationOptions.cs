
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

public record RemoteAuthenticationOptions: AuthenticationOptions
{
  public PathString AccessDeniedPath { get; init; } = "/accessdenied";
  public PathString CallbackPath { get; init; }
  public TimeSpan RemoteAuthenticationTimeout { get; init; } = TimeSpan.FromMinutes(15);
  public string ReturnUrlParameter { get; init; } = "returnUrl";
  public bool SaveTokens { get; init; }
}