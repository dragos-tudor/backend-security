using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal const string UnprotectingTokenFailed = "Unprotecting token failed";
  internal const string TokenExpired = "Token expired";

  static readonly AuthenticateResult UnprotectingTokenFailedResult = Fail(UnprotectingTokenFailed);
  static readonly AuthenticateResult TokenExpiredResult = Fail(TokenExpired);

  public static AuthenticateResult AuthenticateBearerToken (
    HttpContext context,
    BearerTokenDataFormat bearerTokenProtector,
    DateTimeOffset currentUtc)
  {
    var authorization = GetRequestAuthorizationHeader(context.Request);
    var bearerToken = GetAuthorizationBearerToken(authorization);
    if (bearerToken is null) return NoResult();

    var ticket = bearerTokenProtector.Unprotect(bearerToken);
    var expiresUtc = GetAuthenticationTicketExpires(ticket);

    if (expiresUtc is null) return UnprotectingTokenFailedResult;
    if (currentUtc >= expiresUtc) return TokenExpiredResult;

    return Success(ticket!);
  }

  public static Task<AuthenticateResult> AuthenticateBearerToken (HttpContext context)
  {
    var tokenOptions = ResolveRequiredService<BearerTokenOptions>(context);
    var authResult = AuthenticateBearerToken(
      context,
      ResolveRequiredService<BearerTokenDataFormat>(context),
      ResolveRequiredService<TimeProvider>(context).GetUtcNow()
    );

    LogAuthenticationResult(ResolveBearerTokenLogger(context), authResult, tokenOptions.SchemeName, context.TraceIdentifier);
    return Task.FromResult(authResult);
  }
}