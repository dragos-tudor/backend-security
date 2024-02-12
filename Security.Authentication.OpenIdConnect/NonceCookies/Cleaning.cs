using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string CleanNonceCookie(
    HttpContext context,
    NonceCookieBuilder cookieBuilder,
    string cookieName)
  {
    var cookieOptions = cookieBuilder.Build(context, DateTimeOffset.MinValue);
    DeleteNonceCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}