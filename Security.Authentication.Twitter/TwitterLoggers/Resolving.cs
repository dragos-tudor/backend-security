using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  const string TwitterLogger = nameof(Twitter);

  static ILogger ResolveTwitterLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, TwitterLogger);
}