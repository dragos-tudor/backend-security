using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate int UnauthorizeFunc (HttpContext context);