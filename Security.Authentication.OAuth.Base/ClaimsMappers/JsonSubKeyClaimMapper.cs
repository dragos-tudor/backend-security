
namespace Security.Authentication.OAuth;

public record JsonSubKeyClaimMapper(string ClaimType, string JsonKey, string JsonSubKey, string ClaimValueTypes = "string"): JsonKeyClaimMapper(ClaimType, JsonKey, ClaimValueTypes);