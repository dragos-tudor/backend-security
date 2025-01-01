
namespace Security.Conformance.OpenIdConnect;

sealed class TestInstanceInfo : TestInstanceBase
{
  public DateTime Created { get; init; }
  public DateTime Updated { get; init; }
  public string? Error { get; init; }
  public required OpenIdBrowser Browser { get; init; }
  public required OpenIdOwner Owner { get; init; }
  public required OpenIdExposed Exposed { get; init; }
}