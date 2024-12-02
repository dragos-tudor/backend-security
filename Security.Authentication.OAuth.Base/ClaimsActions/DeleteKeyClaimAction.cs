
namespace Security.Authentication.OAuth;

public record DeleteKeyClaimAction(string ClaimType): ClaimAction(ClaimType, "string");

