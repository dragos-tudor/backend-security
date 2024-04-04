
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  internal const string CorrelationCookieMarker = "N";

  static void AppendCorrelationCookie (HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Append(cookieName, CorrelationCookieMarker, cookieOptions);

}