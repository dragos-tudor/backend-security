
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool ExistsCookie(string? cookie) => cookie is not null;

  public static bool IsSecuredCookie(HttpContext context, CookieSecurePolicy securePolicy) =>
    securePolicy == CookieSecurePolicy.Always ||
   (securePolicy == CookieSecurePolicy.SameAsRequest && context.Request.IsHttps);
}