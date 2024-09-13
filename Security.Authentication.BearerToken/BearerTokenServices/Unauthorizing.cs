using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static int UnauthorizeBearerToken (HttpContext context) =>
    UnauthorizeAuth (context, ResolveRequiredService<BearerTokenOptions>(context), ResolveBearerTokenLogger(context));
}