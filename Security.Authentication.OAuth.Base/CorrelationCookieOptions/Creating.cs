
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseFuncs
{
  const string CorrelationCookieName = ".AspNetCore.Correlation.";

  static CookieOptions CreateCorrelationCookieOptions(HttpContext context) =>
    new() {
      HttpOnly = true,
      IsEssential = true,
      SameSite = SameSiteMode.None,
      Secure = context.Request.IsHttps
    };

}