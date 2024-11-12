
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string ChallengeBearerToken(HttpContext context) =>
    ChallengeBearerToken(
      context,
      ResolveRequiredService<BearerTokenOptions>(context),
      ResolveBearerTokenLogger(context));
}