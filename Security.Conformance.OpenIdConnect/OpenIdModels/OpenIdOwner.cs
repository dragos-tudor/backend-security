
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class OpenIdOwner
{
  [JsonPropertyName("sub")]
  public required string Subject { get; init; }
  [JsonPropertyName("iss")]
  public required string Issuer { get; init; }
}