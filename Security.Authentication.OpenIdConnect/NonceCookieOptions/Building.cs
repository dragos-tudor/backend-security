using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static CookieOptions BuildNonceCookieOptions(HttpContext context, CookieBuilder cookieBuilder) =>
    cookieBuilder.Build(context);
}