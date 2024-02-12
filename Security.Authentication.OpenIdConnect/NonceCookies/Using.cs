using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string UseNonceCookie(
    HttpContext context,
    string cookieContent,
    NonceCookieBuilder cookieBuilder,
    DateTimeOffset currentUtc)
  {
    var cookieName = BuildNonceCookieName(cookieBuilder.Name, cookieContent);
    var cookieOptions = cookieBuilder.Build(context, currentUtc);
    AppendNonceCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}