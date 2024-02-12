using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static bool IsValidNonce(
    HttpRequest request,
    string nonce,
    NonceCookieBuilder cookieBuilder,
    OpenIdConnectOptions oidcOptions,
    StringDataFormat stringDataFormat)
  {
    var cookieName = GetNonceCookieName(request.Cookies, cookieBuilder.Name);
    if(cookieName is null) return false;

    var protectedNonce = GetProtectedNonce(cookieName!, cookieBuilder.Name!);
    var unprotectedNonce = stringDataFormat.Unprotect(protectedNonce);
    return unprotectedNonce == nonce;
  }
}