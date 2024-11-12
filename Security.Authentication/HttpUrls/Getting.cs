
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string GetAbsoluteUrl(HttpRequest request, string path) => IsRelativeUri(path)? $"{request.Scheme}{Uri.SchemeDelimiter}{request.Host}{request.PathBase}{path}": path;

  public static string GetRelativeUri(HttpRequest request) => $"{request.PathBase}{request.Path}{request.QueryString}";

  public static string GetRequestPathBase(HttpRequest request) => request.PathBase;

  public static string? GetRequestQueryValue(HttpRequest request, string paramName) => request.Query[paramName];

  public static string GetRequestUrl(HttpRequest request) => $"{request.PathBase}{request.Path}{request.QueryString}";
}