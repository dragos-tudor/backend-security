
namespace Security.Authentication.OAuth;

public record UserInfoResult(ClaimsPrincipal? Principal, string? Failure);