
namespace Security.Authentication.OpenIdConnect;

public partial record class UserInfoResult(ClaimsPrincipal? Success, string? Failure);