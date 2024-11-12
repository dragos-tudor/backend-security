
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static ClaimsPrincipal? SetContextUser(HttpContext context, ClaimsPrincipal? principal) => context.User = principal!;
}