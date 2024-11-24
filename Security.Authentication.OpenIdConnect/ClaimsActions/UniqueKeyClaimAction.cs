
namespace Security.Authentication.OpenIdConnect;

public record UniqueKeyClaimAction(string ClaimType, string JsonKey): ClaimAction(ClaimType);