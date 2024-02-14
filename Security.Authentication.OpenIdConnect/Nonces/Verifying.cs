using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  public static bool IsValidNonce(
    IRequestCookieCollection cookies,
    string nonce,
    OpenIdConnectOptions oidcOptions,
    StringDataFormat stringDataFormat)
  {
    var cookieName = GetNonceCookieName(cookies);
    if (cookieName is null) return false;

    var protectedNonce = GetProtectedNonce(cookieName!);
    var unprotectedNonce = stringDataFormat.Unprotect(protectedNonce);
    return unprotectedNonce == nonce;
  }
}