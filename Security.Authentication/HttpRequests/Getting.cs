
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  public static string? GetRequestQueryReturnUrl (HttpRequest request, string returnUrlParameter) =>
    IsRelativeUri(request.Query[returnUrlParameter])?
      request.Query[returnUrlParameter]:
      default;

}