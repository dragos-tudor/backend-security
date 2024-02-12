using Microsoft.Extensions.Logging;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static readonly ILogger Logger = CreateLogger(GetLoggerFactory(), typeof(CookiesFuncs).Namespace!);
}