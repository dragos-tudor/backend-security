
namespace Security.Authentication;

public abstract record AuthenticationOptions {

  public string ClaimsIssuer { get; init; } = default!;
  public string SchemeName { get; init; } = default!;

}