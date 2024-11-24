using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  public static string DeleteCorrelationCookie(
    HttpContext context,
    OAuthOptions remoteOptions,
    string? correlationId)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    var cookieName = GetCorrelationCookieName(correlationId!);

    SetCookieOptionsPath(cookieOptions, GetCookiePath(context.Request, remoteOptions.CallbackPath));
    DeleteCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}