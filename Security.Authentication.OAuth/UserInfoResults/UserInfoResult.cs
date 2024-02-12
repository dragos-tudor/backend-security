
namespace Security.Authentication.OAuth;

public record class UserInfoResult(ClaimsPrincipal? Principal, string? Failure);