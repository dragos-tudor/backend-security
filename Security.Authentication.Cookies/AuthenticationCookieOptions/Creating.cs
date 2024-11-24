
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static readonly TimeSpan DefaultExpiresAfter = TimeSpan.FromMinutes(30);

  public static AuthenticationCookieOptions CreateAuthenticationCookieOptions(string? cookieName = default, string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
    new() {
      CookieName = cookieName ?? $"{CookieAuthenticationDefaults.CookiePrefix}{CookieAuthenticationDefaults.AuthenticationScheme}",
      ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter,
      ExpireAfter = DefaultExpiresAfter,
      SchemeName = schemeName
    };
}
