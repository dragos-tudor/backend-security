
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class Funcs {

  public static ClaimsPrincipal? SetContextUser (HttpContext context, ClaimsPrincipal? principal) =>
    context.User = principal!;

}