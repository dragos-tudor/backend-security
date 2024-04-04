
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static CookieAuthenticationOptions CreateCookieAuthenticationOptions (string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
    new () {
      AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath,
      ExpireTimeSpan = TimeSpan.FromMinutes(30),
      LoginPath = CookieAuthenticationDefaults.LoginPath,
      LogoutPath = CookieAuthenticationDefaults.LogoutPath,
      ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter,
      SlidingExpiration = true,
      SchemeName = schemeName
    };

}
