
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static Task<AuthenticateResult> AuthenticateBearerToken(HttpContext context) =>
    LogAuthentication(
      ResolveBearerTokenLogger(context),
      AuthenticateBearerToken(
        context,
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<BearerTokenDataFormat>(context)
      ),
      ResolveRequiredService<BearerTokenOptions>(context).SchemeName,
      context.TraceIdentifier
    ).ToTask();
}