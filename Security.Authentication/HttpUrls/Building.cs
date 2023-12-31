
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static string BuildAbsoluteUrl (HttpRequest request, string path) =>
    IsRelativeUri(path)?
      $"{request.Scheme}{Uri.SchemeDelimiter}{request.Host}{request.PathBase}{path}":
      path;

  public static string BuildRelativeUri (HttpRequest request) =>
    $"{request.PathBase}{request.Path}{request.QueryString}";

}