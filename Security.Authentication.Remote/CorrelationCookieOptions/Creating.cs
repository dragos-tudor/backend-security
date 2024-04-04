
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  const string CorrelationCookieName = ".AspNetCore.Correlation.";

  static CookieOptions CreateCorrelationCookieOptions(HttpContext context) =>
    new () {
      HttpOnly = true,
      IsEssential = true,
      SameSite = SameSiteMode.None,
      Secure = context.Request.IsHttps
    };

}