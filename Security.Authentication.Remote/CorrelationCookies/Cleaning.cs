using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string CleanCorrelationCookie(
    HttpContext context,
    RemoteAuthenticationOptions remoteOptions,
    string? correlationId)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    var cookieName = GetCorrelationCookieName(correlationId!);

    SetCorrelationCookieOptionsPath(cookieOptions, GetCorrelationCookieOptionsPath(context.Request, remoteOptions.CallbackPath));
    DeleteCorrelationCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}