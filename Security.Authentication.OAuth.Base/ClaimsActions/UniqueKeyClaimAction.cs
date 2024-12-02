
namespace Security.Authentication.OAuth;

public record UniqueKeyClaimAction(string ClaimType, string JsonKey): ClaimAction(ClaimType);