
namespace Security.Authentication.OAuth;

public record DeleteClaimAction(string ClaimType) : ClaimAction(ClaimType, "string");

