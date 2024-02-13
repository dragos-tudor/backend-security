
namespace Security.Authentication.OpenIdConnect;

public record class UserInfoResult(ClaimsPrincipal? Principal, string? Failure);