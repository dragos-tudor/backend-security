
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class Funcs {

  internal const string CorrelationCookieMarker = "N";

  internal static void AppendCorrelationCookie (HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Append(cookieName, CorrelationCookieMarker, cookieOptions);

}