using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string UseNonce(
    HttpContext context,
    string nonce,
    OpenIdConnectMessage oidcMessage,
    OpenIdConnectOptions oidcOptions,
    StringDataFormat stringDataFormat,
    DateTimeOffset currentUtc)
  {
    var protectedNonce = stringDataFormat.Protect(nonce);
    SetOpenIdConnectMessageNonce(oidcMessage, nonce);
    UseNonceCookie(context, oidcOptions, protectedNonce, currentUtc);
    return protectedNonce;
  }
}