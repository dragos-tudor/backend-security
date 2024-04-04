
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static ClaimsPrincipal? GetContextUser (HttpContext context) =>
    context.User;

}