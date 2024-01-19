using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  private const string NonceContent = "N";

  static void AppendNonceCookie(HttpResponse response, string name, CookieOptions cookieOptions) =>
    response.Cookies.Append(name, NonceContent, cookieOptions);
}