
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static bool IsSecuredCookie (HttpContext context, CookieSecurePolicy securePolicy) =>
    securePolicy == CookieSecurePolicy.Always ||
    (securePolicy == CookieSecurePolicy.SameAsRequest && context.Request.IsHttps);

}