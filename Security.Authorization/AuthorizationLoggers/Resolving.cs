
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  const string CategoryNameLogger = nameof(AuthorizationFuncs);

  static ILogger ResolveAuthorizationLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, CategoryNameLogger);
}