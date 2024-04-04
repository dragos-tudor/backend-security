
namespace Security.Authentication.OpenIdConnect;

public partial record class PostAuthorizationResult(PostAuthorizationInfo? Success, string? Failure);