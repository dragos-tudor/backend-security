
namespace Security.Authentication.OpenIdConnect;

public record DeleteKeyClaimAction(string ClaimType): ClaimAction(ClaimType, "string");

