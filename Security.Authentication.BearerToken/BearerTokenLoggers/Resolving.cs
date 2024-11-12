
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  const string BearerTokenLogger = nameof(BearerToken);

  static ILogger ResolveBearerTokenLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, BearerTokenLogger);
}