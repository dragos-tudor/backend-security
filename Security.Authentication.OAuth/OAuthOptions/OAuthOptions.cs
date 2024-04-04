
using System.Collections.Generic;
using Security.Authentication.Remote;

namespace Security.Authentication.OAuth;

public record OAuthOptions: RemoteAuthenticationOptions
{
  public required string AuthorizationEndpoint { get; init; }
  public required string TokenEndpoint { get; init; }
  public required string UserInformationEndpoint { get; init; }

  public required string ClientId { get; init; }
  public required string ClientSecret { get; init; }

  public IEnumerable<string>? Scope { get; init; }
  public char ScopeSeparator { get; init; } = ' ';
}
