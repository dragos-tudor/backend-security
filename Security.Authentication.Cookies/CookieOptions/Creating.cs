
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static CookieOptions CreateCookieOptions () =>
    new () {
      HttpOnly = true,
      IsEssential = true,
      Path = "/",
      SameSite = SameSiteMode.Lax,
      Secure = true
    };
}