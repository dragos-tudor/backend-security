
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs {

  const string AuthorizationMiddlewareInvokedWithEndpointKey = "__AuthorizationMiddlewareWithEndpointInvoked";

  static Endpoint? MarkEndpointInvoked(HttpContext context, Endpoint? endpoint) =>
    endpoint is not null?
      (Endpoint) (context.Items[AuthorizationMiddlewareInvokedWithEndpointKey] = endpoint):
      default;

}