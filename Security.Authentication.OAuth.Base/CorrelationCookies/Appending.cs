
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  internal const string CorrelationCookieMarker = "N";

  static void AppendCorrelationCookie(HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    AppendCookie(response, cookieName, CorrelationCookieMarker, cookieOptions);
}