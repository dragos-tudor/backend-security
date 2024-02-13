
namespace Security.Authentication.OAuth;

public record class UserInfoResult(ClaimsPrincipal? Success, string? Failure);