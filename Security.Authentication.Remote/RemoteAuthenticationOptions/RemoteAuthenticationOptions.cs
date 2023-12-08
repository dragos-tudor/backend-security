
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

public record RemoteAuthenticationOptions: AuthenticationOptions {

  public PathString AccessDeniedPath { get; init; } = default!;
  public PathString CallbackPath { get; init; } = default!;
  public TimeSpan RemoteAuthenticationTimeout { get; init; } = TimeSpan.FromMinutes(15);
  public string ReturnUrlParameter { get; init; } = "ReturnUrl";
  public bool SaveTokens { get; init; } = false;

  public HttpClient RemoteClient { get; init; } = default!;

}