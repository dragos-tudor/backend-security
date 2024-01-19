
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static string? GetRequestQueryReturnUrl (HttpRequest request, string returnUrlParameter) =>
    IsRelativeUri(request.Query[returnUrlParameter])?
      request.Query[returnUrlParameter]:
      default;

  public static string GetRequestUrl (HttpRequest request) =>
    $"{request.PathBase}{request.Path}{request.QueryString}";

}