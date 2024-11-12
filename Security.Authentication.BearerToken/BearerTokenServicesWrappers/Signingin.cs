using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static Task<AuthenticationTicket> SignInBearerToken(
    HttpContext context,
    ClaimsPrincipal principal,
    AuthenticationProperties? authProperties = default) =>
      SignInBearerToken(
        context,
        principal,
        authProperties ?? new AuthenticationProperties(),
        ResolveRequiredService<BearerTokenOptions>(context),
        ResolveRequiredService<TimeProvider>(context).GetUtcNow(),
        ResolveRequiredService<BearerTokenDataFormat>(context),
        ResolveRequiredService<RefreshTokenDataFormat>(context),
        ResolveBearerTokenLogger(context));
}