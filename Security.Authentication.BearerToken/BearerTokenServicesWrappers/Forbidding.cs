
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string ForbidBearerToken(HttpContext context) =>
    ForbidBearerToken(
      context,
      ResolveRequiredService<BearerTokenOptions>(context),
      ResolveBearerTokenLogger(context));
}