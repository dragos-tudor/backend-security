
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static CookieBuilder CreateCookieBuilder() => new () {
    // "Cookie.Expiration is ignored, use ExpireTimeSpan instead."
    HttpOnly = true,
    IsEssential = true,
    Name = $"{CookieAuthenticationDefaults.CookiePrefix}{CookieAuthenticationDefaults.AuthenticationScheme}",
    Path = "/",
    SameSite = SameSiteMode.Lax,
    SecurePolicy = CookieSecurePolicy.SameAsRequest
  };

}