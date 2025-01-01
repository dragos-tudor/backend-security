
namespace Security.Conformance.OpenIdConnect;

sealed class TestPlanConfig
{
  public string? Description { get; init; }
  public string? Alias { get; init; }
  public required OpenIdClient Client { get; init; }
}