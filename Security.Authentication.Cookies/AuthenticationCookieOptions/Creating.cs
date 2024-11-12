
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static AuthenticationCookieOptions CreateAuthenticationCookieOptions(string? cookieName = default, string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
    new() {
      CookieName = cookieName ?? $"{CookieAuthenticationDefaults.CookiePrefix}{CookieAuthenticationDefaults.AuthenticationScheme}",
      AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath,
      LoginPath = CookieAuthenticationDefaults.LoginPath,
      LogoutPath = CookieAuthenticationDefaults.LogoutPath,
      ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter,
      ExpireTimeSpan = TimeSpan.FromMinutes(30),
      SlidingExpiration = true,
      SchemeName = schemeName
    };
}
