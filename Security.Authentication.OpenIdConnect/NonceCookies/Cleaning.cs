using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static string CleanNonceCookie(
    HttpContext context,
    OpenIdConnectOptions oidcOptions)
  {
    var cookieBuilder = CreateNonceCookieBuilder(context.Request, oidcOptions);
    var cookieOptions = BuildNonceCookieOptions(context, cookieBuilder);
    var cookieName = GetNonceCookieName(context.Request.Cookies);

    SetNonceCookieOptionsExpires(cookieOptions, DateTimeOffset.MinValue);
    DeleteNonceCookie(context.Response, cookieName!, cookieOptions);
    return cookieName!;
  }
}