
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static string GetCookieName (CookieBuilder cookieBuilder, CookieAuthenticationOptions authOptions) =>
    IsDefaultAuthenticationSchemeName(authOptions)?
      cookieBuilder.Name!:
      authOptions.SchemeName;

}