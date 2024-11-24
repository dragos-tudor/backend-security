using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string UseCorrelationCookie(
    HttpContext context,
    OAuthOptions authOptions,
    string correlationId,
    DateTimeOffset currentUtc)
  {
    var cookieName = GetCorrelationCookieName(correlationId);
    var cookieOptions = BuildCorrelationCookieOptions(context, authOptions, currentUtc);

    AppendCorrelationCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}