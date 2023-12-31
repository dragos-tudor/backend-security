
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

public record RemoteAuthenticationOptions: AuthenticationOptions
{
  /// <summary>
  /// Gets or sets the optional path the user agent is redirected to if the user
  /// doesn't approve the authorization demand requested by the remote server.
  /// </summary>
  public PathString AccessDeniedPath { get; init; } = "/accessdenied";
  public required PathString CallbackPath { get; init; }
  public PathString ErrorPath { get; init; } = "/error";
  /// <summary>
  /// Gets or sets the time limit for completing the authentication flow (15 minutes by default).
  /// </summary>
  public TimeSpan RemoteAuthenticationTimeout { get; init; } = TimeSpan.FromMinutes(15);
  /// <summary>
  /// Gets or sets the name of the parameter used to convey the original location
  /// of the user before the remote challenge was triggered up to the access denied page.
  /// This property is only used when the <see cref="AccessDeniedPath"/> is explicitly specified.
  /// </summary>
  // Note: this deliberately matches the default parameter name used by the cookie handler.
  public string ReturnUrlParameter { get; init; } = "returnUrl";
  public bool SaveTokens { get; init; }
}