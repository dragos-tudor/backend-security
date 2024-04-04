
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Testing;

partial class Funcs {

  static StringValues GetResponseCookies (HttpResponse response) =>
    response.Headers.SetCookie;

  public static string? GetResponseCookie(HttpResponse response, string cookieName) =>
    GetResponseCookies(response)
      .FirstOrDefault(IsCookieStartingWithName(cookieName)!);

}