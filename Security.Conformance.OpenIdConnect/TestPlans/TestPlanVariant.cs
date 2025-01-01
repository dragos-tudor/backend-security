
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class TestPlanVariant
{
  [JsonPropertyName("request_type")]
  public required string RequestType { get; init; }
  [JsonPropertyName("client_registration")]
  public required string ClientRegistration { get; init; }
}