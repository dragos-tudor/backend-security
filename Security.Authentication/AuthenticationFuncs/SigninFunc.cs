using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate AuthenticationTicket SignInFunc(
  HttpContext context,
  ClaimsPrincipal principal,
  AuthenticationProperties authProperties);
