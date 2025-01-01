
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class OpenIdClient
{
  [JsonPropertyName("client_id")]
  public required string ClientId { get; init; }
  [JsonPropertyName("client_secret")]
  public required string ClientSecret { get; init; }
  [JsonPropertyName("redirect_uri")]
  public required string RedirectUri { get; init; }
}