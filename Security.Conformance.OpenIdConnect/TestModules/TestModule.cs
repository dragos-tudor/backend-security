
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class TestModule
{
  [JsonPropertyName("testModule")]
  public required string ModuleName { get; init; }
  [JsonPropertyName("variant")]
  public required TestModuleVariant ModuleVariant { get; init; }
  public string[] Instances { get; init; } = [];
}