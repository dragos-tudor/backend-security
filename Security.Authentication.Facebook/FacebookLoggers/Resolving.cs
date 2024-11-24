using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  const string FacebookLogger = nameof(Facebook);

  static ILogger ResolveFacebookLogger(HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, FacebookLogger);
}