using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static string? ChallengeBearerToken (HttpContext context) =>
    ChallengeAuth (SetWWWAuthenticateResponseHeader(context, "Bearer"), ResolveRequiredService<BearerTokenOptions>(context), ResolveBearerTokenLogger(context));
}