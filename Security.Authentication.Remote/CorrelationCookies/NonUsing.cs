using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string NonUseCorrelationCookie(
    HttpContext context,
    RemoteAuthenticationOptions remoteOptions,
    string? correlationId)
  {
    var cookieOptions = ResetCorrelationCookieOptions(context, remoteOptions!);
    var cookieName = GetCorrelationCookieName(correlationId!);
    DeleteCorrelationCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}