using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static void DeleteNonceCookie(HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Delete(cookieName, cookieOptions);
}