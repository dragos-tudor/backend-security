
namespace Security.Authentication;

public abstract record AuthenticationOptions {

  public string SchemeName { get; init; } = default!;

}