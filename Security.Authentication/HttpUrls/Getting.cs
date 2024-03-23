
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string GetRequestPathBase (HttpRequest request) =>
    request.PathBase;

  public static string? GetRequestQueryValue (HttpRequest request, string paramName) =>
    request.Query[paramName];

  public static string GetRequestUrl (HttpRequest request) =>
    $"{request.PathBase}{request.Path}{request.QueryString}";
}