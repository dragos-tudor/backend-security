using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate void ForbidFunc(HttpContext context);