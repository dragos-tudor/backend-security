using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string UseCorrelationCookie(
    HttpContext context,
    string correlationId,
    RemoteAuthenticationOptions remoteOptions,
    DateTimeOffset currentUtc)
  {
    var cookieName = GetCorrelationCookieName(correlationId);
    var cookieOptions = BuildCorrelationCookieOptions(context, remoteOptions, currentUtc);
    AppendCorrelationCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}