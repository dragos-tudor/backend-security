
namespace Security.Authentication;

public abstract record AuthenticationOptions
{
  public string ReturnUrlParameter { get; init; } = "returnUrl";
  public required string SchemeName { get; init; } = default!;
}