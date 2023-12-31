using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

public delegate AuthenticationTicket SignOutFunc(
  HttpContext context,
  AuthenticationProperties authProperties);
