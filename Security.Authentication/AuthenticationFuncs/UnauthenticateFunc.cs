using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate int UnauthenticateFunc (HttpContext context);