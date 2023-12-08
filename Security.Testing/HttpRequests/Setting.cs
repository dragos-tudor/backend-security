
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Testing;

partial class Funcs {

  public static StringValues SetRequestCookiesHeader (HttpRequest request, HttpResponse response) =>
    request.Headers.Cookie = response.Headers.SetCookie;

  public static IRequestCookieCollection SetRequestCookies (HttpRequest request, IRequestCookieCollection cookies) =>
    request.Cookies = cookies;

}

