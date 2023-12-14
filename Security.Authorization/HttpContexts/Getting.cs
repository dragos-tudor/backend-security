
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  public static ClaimsPrincipal? GetContextUser (HttpContext context) =>
    context.User;

}