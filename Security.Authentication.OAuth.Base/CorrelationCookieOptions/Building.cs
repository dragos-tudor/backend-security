
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs {

  static CookieOptions BuildCorrelationCookieOptions(
    HttpContext context,
    OAuthOptions oauthOptions,
    DateTimeOffset currentUtc)
  {
    var cookieOptions = CreateCorrelationCookieOptions(context);
    var cookieOptionsExpires = GetCorrelationCookieOptionsExpires(oauthOptions, currentUtc);

    SetCookieOptionsExpires(cookieOptions, cookieOptionsExpires);
    SetCookieOptionsPath(cookieOptions, GetCookiePath(context.Request, oauthOptions.CallbackPath));
    return cookieOptions;
  }


}