

namespace Security.Conformance.OpenIdConnect;

sealed class OpenIdExposed
{
  public required string Issuer { get; init; }
  public required string DiscoveryUrl { get; init; }
}