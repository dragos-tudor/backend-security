
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? GetResponseLocation(HttpResponse response) => response.Headers.Location;
}