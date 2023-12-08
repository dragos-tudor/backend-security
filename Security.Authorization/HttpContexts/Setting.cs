
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  public static ClaimsPrincipal? SetContextUser (HttpContext context, ClaimsPrincipal? principal) =>
    context.User = principal!;

}