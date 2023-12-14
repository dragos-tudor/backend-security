
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static string? GetResponseLocation (HttpResponse response) =>
    response.Headers.Location;

}