
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  public static ClaimsPrincipal? GetContextUser (HttpContext context) =>
    context.User;

}