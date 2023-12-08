
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

public delegate string ChallengeSchemeFunc(HttpContext context, string? schemeName = default);