
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  readonly static string BearerTokenLogger = typeof(BearerTokenFuncs).FullName!;

  static ILogger ResolveBearerTokenLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, BearerTokenLogger);
}