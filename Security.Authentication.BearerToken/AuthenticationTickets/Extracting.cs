
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal const string NoToken = "no token";
  internal const string UnprotectingTokenFailed = "unprotecting token failed";

  static(AuthenticationTicket? authTicket, string? error) ExtractAuthenticationTicket(HttpContext context, BearerTokenDataFormat bearerTokenProtector)
  {
    var authorization = GetHttpRequestAuthorization(context.Request);
    var bearerToken = GetHttpRequestBearerToken(authorization);
    if(bearerToken is null) return(default, NoToken);

    var authTicket = bearerTokenProtector.Unprotect(bearerToken);
    if(authTicket is null) return(default, UnprotectingTokenFailed);

    return(authTicket, default);
  }
}