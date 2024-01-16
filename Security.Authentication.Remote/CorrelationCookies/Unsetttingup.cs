
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static string UnsetupCorrelationCookie (
    HttpContext context,
    RemoteAuthenticationOptions remoteOptions,
    string correlationId)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    SetCorrelationCookieOptionsPath(cookieOptions, GetCorrelationCookieOptionsPath(context.Request, remoteOptions.CallbackPath));
    DeleteCorrelationCookie(context.Response, GetCorrelationCookieName(correlationId), cookieOptions);
    return GetCorrelationCookieName(correlationId);
  }

}