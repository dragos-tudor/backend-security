
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

abstract class TestInstanceBase
{
  [JsonPropertyName("id")]
  public required string InstanceId { get; init; }
  public required string Name { get; init; }
}

sealed class TestInstance : TestInstanceBase
{
  public required string Url { get; init; }
}