
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  readonly static string CookiesLogger = typeof(CookiesFuncs).FullName!;

  static ILogger ResolveCookiesLogger(HttpContext context) => ResolveLogger(context.RequestServices, CookiesLogger);
}