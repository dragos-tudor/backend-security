
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static bool IsDefaultAuthenticationSchemeName (CookieAuthenticationOptions authOptions) =>
    authOptions.SchemeName == CookieAuthenticationDefaults.AuthenticationScheme;

}