
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public abstract record AuthenticationOptions
{
  public PathString AccessDeniedPath { get; init; } = "/accessdenied";
  public PathString LoginPath { get; init; } = "/login";
  public PathString ErrorPath { get; init; } = "/error";

  public string ReturnUrlParameter { get; init; } = "returnUrl";
  public required string SchemeName { get; init; } = default!;
}