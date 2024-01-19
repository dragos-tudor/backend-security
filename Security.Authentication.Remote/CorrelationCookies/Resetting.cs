
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static CookieOptions ResetCorrelationCookie (
    HttpContext context,
    RemoteAuthenticationOptions remoteOptions)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    SetCorrelationCookieOptionsPath(cookieOptions, GetCorrelationCookieOptionsPath(context.Request, remoteOptions.CallbackPath));
    return cookieOptions;
  }

}