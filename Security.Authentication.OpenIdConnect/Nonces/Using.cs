using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string UseNonce(
    HttpContext context,
    string nonce,
    OpenIdConnectMessage authMessage,
    NonceCookieBuilder cookieBuilder,
    StringDataFormat stringDataFormat,
    DateTimeOffset currentUtc)
  {
    var protectedNonce = stringDataFormat.Protect(nonce);
    SetOpenIdConnectMessageNonce(authMessage, nonce);
    UseNonceCookie(context, protectedNonce, cookieBuilder, currentUtc);
    return protectedNonce;
  }
}