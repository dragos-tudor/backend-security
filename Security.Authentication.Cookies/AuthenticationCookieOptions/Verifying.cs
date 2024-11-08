
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsDefaultAuthenticationSchemeName (AuthenticationCookieOptions authOptions) =>
    authOptions.SchemeName == CookieAuthenticationDefaults.AuthenticationScheme;
}