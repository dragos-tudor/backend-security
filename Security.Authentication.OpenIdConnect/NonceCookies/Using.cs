using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string UseNonceCookie(
    HttpContext context,
    OpenIdConnectOptions oidcOptions,
    string nonce,
    DateTimeOffset currentUtc)
  {
    var cookieName = BuildNonceCookieName(nonce);
    var cookieBuilder = CreateNonceCookieBuilder(context.Request, oidcOptions);
    var cookieOptions = BuildNonceCookieOptions(context, cookieBuilder);

    SetNonceCookieOptionsExpires(cookieOptions, currentUtc.Add(oidcOptions.NonceLifetime));
    AppendNonceCookie(context.Response, cookieName, cookieOptions);
    return cookieName;
  }
}