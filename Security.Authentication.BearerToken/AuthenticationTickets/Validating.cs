
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal const string MissingTokenExpires = "missing token expires";
  internal const string TokenExpired = "token expired";

  static string? ValidateAuthenticationTicket(DateTimeOffset currentUtc, AuthenticationTicket authTicket)
  {
    var expiresUtc = GetAuthenticationTicketExpires(authTicket!);
    if(expiresUtc is null) return MissingTokenExpires;
    if(currentUtc >= expiresUtc) return TokenExpired;
    return default;
  }
}