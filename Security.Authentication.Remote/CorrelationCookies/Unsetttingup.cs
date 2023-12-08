
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class Funcs {

  public static string UnsetupCorrelationCookie (
    HttpContext context,
    RemoteAuthenticationOptions remoteOptions,
    string correlationId)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    SetCorrelationCookieOptionsPath(cookieOptions, GetCorrelationCookieOptionsPath(context.Request, remoteOptions.CallbackPath));
    UnsetResponseCookieHeader(context.Response, cookieOptions, correlationId);
    return GetCorrelationCookieName(correlationId);
  }

}