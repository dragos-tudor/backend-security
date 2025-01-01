
namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string GetAbsoluteUrl(HttpRequest request, string path) => IsRelativeUri(path)? $"{request.Scheme}{Uri.SchemeDelimiter}{request.Host}{request.PathBase}{path}": path;

  public static string GetRelativeUri(HttpRequest request) => $"{request.PathBase}{request.Path}{request.QueryString}";

  public static string GetHttpRequestFullPath(HttpRequest request) => $"{request.PathBase}{request.Path}{request.QueryString}";

  public static string GetHttpRequestPathBase(HttpRequest request) => request.PathBase;

  public static string? GetHttpRequestQueryValue(HttpRequest request, string paramName) => request.Query[paramName];
}