
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string CategoryNameLogger = nameof(OAuthFuncs);

  static ILogger ResolveOAuthLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, CategoryNameLogger);
}