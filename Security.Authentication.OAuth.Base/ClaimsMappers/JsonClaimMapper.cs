
namespace Security.Authentication.OAuth;

public record JsonClaimMapper(string ClaimType, string JsonKey, string ClaimValueTypes = "string") : ClaimMapper(ClaimType, JsonKey, ClaimValueTypes);

