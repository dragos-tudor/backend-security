using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  const string GoogleLogger = nameof(Google);

  static ILogger ResolveGoogleLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, GoogleLogger);
}