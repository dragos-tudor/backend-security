
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

public delegate string ForbidSchemeFunc(HttpContext context, string? schemeName = default);