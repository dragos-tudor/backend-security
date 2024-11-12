
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static AuthenticateResult AuthenticateBearerToken(
    HttpContext context,
    DateTimeOffset currentUtc,
    BearerTokenDataFormat bearerTokenProtector)
  {
    var(authTicket, error) = ExtractAuthenticationTicket(context, bearerTokenProtector);
    if(error == NoToken) return NoResult();
    if(error is not null) return Fail(error);

    var validationError = ValidateAuthenticationTicket(currentUtc, authTicket!);
    if(validationError is not null) return Fail(validationError);

    return Success(authTicket!);
  }

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