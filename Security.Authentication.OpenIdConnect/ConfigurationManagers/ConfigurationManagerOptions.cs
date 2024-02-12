namespace Security.Authentication.OpenIdConnect;

public class ConfigurationManagerOptions
{
  public required string MetadataAddress { get; init; }
  public required TimeSpan RefreshInterval { get; init; }
  public TimeSpan AutomaticRefreshInterval { get; init; }
}