using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static int UnauthenticateBearerToken (HttpContext context) =>
    UnauthenticateAuth (SetWWWAuthenticateResponseHeader(context, "Bearer"), ResolveRequiredService<BearerTokenOptions>(context), ResolveBearerTokenLogger(context));
}