
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class Funcs {

  internal const string AccessDeniedAuthorizationError = "access_denied";
  internal const string AuthorizationError = "error";

  static bool IsAccessDeniedAuthorizationError (HttpRequest request) =>
    request.Query[AuthorizationError] == AccessDeniedAuthorizationError;

  static bool IsGenericAuthorizationError (HttpRequest request) =>
    IsNotEmptyString(request.Query[AuthorizationError]);

}