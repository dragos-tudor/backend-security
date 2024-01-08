using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static Microsoft.AspNetCore.Authentication.AuthenticateResult;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static readonly AuthenticateResult FailedUnprotectingToken = Fail("Unprotected token failed");
  static readonly AuthenticateResult TokenExpired = Fail("Token expired");

  public static AuthenticateResult AuthenticateBearerToken(
    HttpContext context,
    SecureDataFormat<AuthenticationTicket> bearerTokenProtector,
    DateTimeOffset currentUtc)
  {
    var token = GetRequestBearerToken(context.Request);
    if (token is null) return NoResult();

    var ticket = bearerTokenProtector.Unprotect(token);
    if (GetAuthenticationTicketExpires(ticket) is not {} expiresUtc) return FailedUnprotectingToken;
    if (currentUtc >= expiresUtc) return TokenExpired;

    return Success(ticket!);
  }
}