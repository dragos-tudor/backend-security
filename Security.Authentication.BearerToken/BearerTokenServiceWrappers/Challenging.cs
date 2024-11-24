
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static void ChallengeBearerToken(HttpContext context) =>
    ChallengeBearerToken(
      context,
      ResolveRequiredService<BearerTokenOptions>(context),
      ResolveBearerTokenLogger(context));
}