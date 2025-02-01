
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  readonly static string AuthorizationLogger = typeof(AuthorizationFuncs).FullName!;

  static ILogger ResolveAuthorizationLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, AuthorizationLogger);
}