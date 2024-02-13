using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  const string NonceCookieContent = "N";

  static void AppendNonceCookie(HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Append(cookieName, NonceCookieContent, cookieOptions);
}