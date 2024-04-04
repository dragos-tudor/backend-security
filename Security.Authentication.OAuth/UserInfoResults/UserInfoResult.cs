
namespace Security.Authentication.OAuth;

public partial record class UserInfoResult(ClaimsPrincipal? Success, string? Failure);