using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate string? ChallengeFunc (HttpContext context, AuthenticationProperties? authProperties = default);