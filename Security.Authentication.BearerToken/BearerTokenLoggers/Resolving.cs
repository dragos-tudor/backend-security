
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  const string CategoryNameLogger = nameof(BearerTokenFuncs);

  static ILogger ResolveBearerTokenLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, CategoryNameLogger);
}