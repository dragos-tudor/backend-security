
using System.Collections.Generic;
using Security.Authentication.Remote;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

public record OAuthOptions: RemoteAuthenticationOptions
{
  public required string AuthorizationEndpoint { get; init; }
  public required string TokenEndpoint { get; init; }
  public required string UserInformationEndpoint { get; init; }

  public ClaimActionCollection ClaimActions { get; init; } = [];
  public string? ClaimsIssuer { get; init; }
  public required PathString ChallengePath { get; init; }

  public required string ClientId { get; init; }
  public required string ClientSecret { get; init; }

  public IEnumerable<string>? Scope { get; init; }
  public char ScopeSeparator { get; init; } = ' ';
  public bool UsePkce { get; init; }
}
