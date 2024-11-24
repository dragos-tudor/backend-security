
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs {

  static CookieOptions BuildCorrelationCookieOptions(
    HttpContext context,
    OAuthOptions authOptions,
    DateTimeOffset currentUtc)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    var cookieOptionsExpires = GetCorrelationCookieOptionsExpires(authOptions, currentUtc);

    SetCookieOptionsExpires(cookieOptions, cookieOptionsExpires);
    SetCookieOptionsPath(cookieOptions, GetCookiePath(context.Request, authOptions.CallbackPath));
    return cookieOptions;
  }


}