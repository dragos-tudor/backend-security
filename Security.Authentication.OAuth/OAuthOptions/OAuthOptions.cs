
using System.Collections.Generic;
using Auth = Microsoft.AspNetCore.Authentication;
using Security.Authentication.Remote;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;

namespace Security.Authentication.OAuth;

public record OAuthOptions: RemoteAuthenticationOptions {

  public string AuthorizationEndpoint { get; init; } = default!;
  public ClaimActionCollection ClaimActions { get; init; } = default!;
  public string ClientId { get; init; } = default!;
  public string ClientSecret { get; init; } = default!;
  public RemoteAuthenticationOptions RemoteOptions { get; init; } = default!;
  public IEnumerable<string> Scope { get; init; } = default!;
  public char ScopeSeparator { get; init; } = default!;
  public string TokenEndpoint { get; init; } = default!;
  public string UserInformationEndpoint { get; init; } = default!;
  public bool UsePkce { get; init; }

  public Auth.ISecureDataFormat<Auth.AuthenticationProperties> StateDataFormat { get; init; } = default!;
}
