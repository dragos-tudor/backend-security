
namespace Security.Authentication.OAuth;

public record JsonKeyClaimMapper(string ClaimType, string JsonKey, string ClaimValueTypes = "string"): ClaimMapper(ClaimType, ClaimValueTypes);

