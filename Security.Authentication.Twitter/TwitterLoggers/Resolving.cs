using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Twitter;

partial class TwitterFuncs
{
  readonly static string TwitterLogger = typeof(TwitterFuncs).FullName!;

  static ILogger ResolveTwitterLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, TwitterLogger);
}