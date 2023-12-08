
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static CookieOptions BuildCookieOptions(CookieBuilder cookieBuilder, HttpContext context) =>
    cookieBuilder.Build(context);

}