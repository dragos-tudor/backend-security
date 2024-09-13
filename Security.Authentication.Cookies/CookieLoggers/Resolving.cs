
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  const string CookiesLogger = nameof(Cookies);

  static ILogger ResolveCookiesLogger (HttpContext httpContext) => ResolveLogger(httpContext.RequestServices, CookiesLogger);
}