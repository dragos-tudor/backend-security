
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate AuthenticateResult AuthenticateFunc(HttpContext context);