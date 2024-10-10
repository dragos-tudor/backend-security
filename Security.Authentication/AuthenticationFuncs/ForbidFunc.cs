using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate string ForbidFunc (HttpContext context, AuthenticationProperties? authProperties = default);