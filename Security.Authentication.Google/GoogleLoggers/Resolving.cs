using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  readonly static string GoogleLogger = typeof(GoogleFuncs).FullName!;

  static ILogger ResolveGoogleLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, GoogleLogger);
}