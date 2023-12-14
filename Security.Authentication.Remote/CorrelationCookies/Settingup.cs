
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static string SetupCorrelationCookie (
    HttpContext context,
    RemoteAuthenticationOptions authOptions,
    DateTimeOffset currentUtc,
    string correlationId)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    SetCorrelationCookieOptionsExpires(cookieOptions, GetCorrelationCookieOptionsExpires(cookieOptions, authOptions.RemoteAuthenticationTimeout, currentUtc));
    SetCorrelationCookieOptionsPath(cookieOptions, GetCorrelationCookieOptionsPath(context.Request, authOptions.CallbackPath));
    SetResponseCookieHeader(context.Response, cookieOptions, correlationId);
    return GetCorrelationCookieName(correlationId);
  }


}