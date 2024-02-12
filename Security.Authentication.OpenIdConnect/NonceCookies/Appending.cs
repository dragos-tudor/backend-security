using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  private const string NonceContent = "N";

  static void AppendNonceCookie(HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Append(cookieName, NonceContent, cookieOptions);
}