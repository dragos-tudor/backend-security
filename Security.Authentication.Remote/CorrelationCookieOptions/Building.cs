
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  static CookieOptions BuildCorrelationCookieOptions (
    HttpContext context,
    RemoteAuthenticationOptions remoteOptions,
    DateTimeOffset currentUtc)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    SetCorrelationCookieOptionsExpires(cookieOptions, GetCorrelationCookieOptionsExpires(cookieOptions, remoteOptions.RemoteAuthenticationTimeout, currentUtc));
    SetCorrelationCookieOptionsPath(cookieOptions, GetCorrelationCookieOptionsPath(context.Request, remoteOptions.CallbackPath));
    return cookieOptions;
  }


}