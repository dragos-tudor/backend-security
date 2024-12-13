
using System.Text.Json.Serialization;

namespace Security.Conformance.OpenIdConnect;

sealed class TestPlan
{
  [JsonPropertyName("_id")]
  public required string PlanId { get; init; }
  public required string PlanName { get; init; }
  public required string Description { get; init; }
  public required string CertificationProfileName { get; init; }

  public bool? Immutable { get; init; }
  public bool? Publish { get; init; }
  public required DateTime Started { get; init; }
  public string? Summary { get; init; }
  public string? Version { get; init; }

  public required OpenIdOwner Owner { get; init; }
  [JsonPropertyName("variant")]
  public required TestPlanVariant PlanVariant { get; init; }
  [JsonPropertyName("config")]
  public required TestPlanConfig PlanConfig { get; init; }
  [JsonPropertyName("modules")]
  public TestModule[] TestModules { get; init; } = [];
}

sealed class TestPlanVariant
{
  [JsonPropertyName("request_type")]
  public required string RequestType { get; init; }
  [JsonPropertyName("client_registration")]
  public required string ClientRegistration { get; init; }
}

sealed class TestPlanConfig
{
  public string? Description { get; init; }
  public string? Alias { get; init; }
  public required OpenIdClient Client { get; init; }
}

sealed class TestPlans
{
  [JsonPropertyName("data")]
  public TestPlan[] Plans { get; init; } = [];
  public int RecordsTotal { get; init; }
  public int RecordsFiltered { get; init; }
}