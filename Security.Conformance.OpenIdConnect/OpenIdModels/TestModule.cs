
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class TestModule
{
  [JsonPropertyName("testModule")]
  public required string ModuleName { get; init; }
  [JsonPropertyName("variant")]
  public required TestModuleVariant ModuleVariant { get; init; }
}

sealed class TestModuleVariant
{
  [JsonPropertyName("client_auth_type")]
  public required string ClientAuthType { get; init; }
  [JsonPropertyName("response_type")]
  public required string ResponseType { get; init; }
  [JsonPropertyName("response_mode")]
  public required string ResponseMode { get; init; }
}