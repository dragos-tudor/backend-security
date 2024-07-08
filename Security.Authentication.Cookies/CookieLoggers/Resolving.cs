
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string CategoryNameLogger = nameof(CookiesFuncs);

  static ILogger ResolveCookiesLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, CategoryNameLogger);
}