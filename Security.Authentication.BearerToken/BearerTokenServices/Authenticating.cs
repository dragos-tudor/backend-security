using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  const string UnprotectingTokenFailed = "Unprotecting token failed";
  const string TokenExpired = "Token expired";

  static readonly AuthenticateResult UnprotectingTokenFailedResult = Fail(UnprotectingTokenFailed);
  static readonly AuthenticateResult TokenExpiredResult = Fail(TokenExpired);

  public static AuthenticateResult AuthenticateBearerToken(
    HttpContext context,
    IBearerTokenProtector bearerTokenProtector,
    DateTimeOffset currentUtc)
  {
    var token = GetRequestBearerToken(context.Request);
    if (token is null) return NoResult();

    var ticket = bearerTokenProtector.Unprotect(token);
    var expiresUtc = GetAuthenticationTicketExpires(ticket);

    if (expiresUtc is null) return UnprotectingTokenFailedResult;
    if (currentUtc >= expiresUtc) return TokenExpiredResult;

    return Success(ticket!);
  }

  public static Task<AuthenticateResult> AuthenticateBearerToken (HttpContext context)
  {
    var authOptions = ResolveService<BearerTokenOptions>(context);
    var authResult = AuthenticateBearerToken(
      context,
      ResolveService<IBearerTokenProtector>(context),
      ResolveService<TimeProvider>(context).GetUtcNow()
    );

    LogAuthenticationResult(Logger, authResult, authOptions.SchemeName, context.TraceIdentifier);
    return Task.FromResult(authResult);
  }
}