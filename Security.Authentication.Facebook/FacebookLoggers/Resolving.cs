using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  readonly static string FacebookLogger = typeof(FacebookFuncs).FullName!;

  static ILogger ResolveFacebookLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, FacebookLogger);
}