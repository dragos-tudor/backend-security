
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static string? GetResponseLocation (HttpResponse response) =>
    response.Headers.Location;

}