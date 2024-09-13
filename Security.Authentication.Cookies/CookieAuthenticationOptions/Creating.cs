
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  public static CookieAuthenticationOptions CreateCookieAuthenticationOptions (string schemeName = CookieAuthenticationDefaults.AuthenticationScheme) =>
    new () {
      AccessDeniedPath = CookieAuthenticationDefaults.AccessDeniedPath,
      LoginPath = CookieAuthenticationDefaults.LoginPath,
      LogoutPath = CookieAuthenticationDefaults.LogoutPath,
      ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter,
      ExpireTimeSpan = TimeSpan.FromMinutes(30),
      SlidingExpiration = true,
      SchemeName = schemeName
    };

}
