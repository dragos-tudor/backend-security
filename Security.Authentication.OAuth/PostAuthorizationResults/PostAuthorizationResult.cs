using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

public partial record class PostAuthorizationResult(AuthenticationProperties? Success, string? Failure = default);