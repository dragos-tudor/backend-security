
using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class Funcs {

  const string AuthorizationMiddlewareInvokedWithEndpointKey = "__AuthorizationMiddlewareWithEndpointInvoked";

  static Endpoint? MarkEndpointInvoked(HttpContext context, Endpoint? endpoint) =>
    endpoint is not null?
      (Endpoint) (context.Items[AuthorizationMiddlewareInvokedWithEndpointKey] = endpoint):
      default;

}