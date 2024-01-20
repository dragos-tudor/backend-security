using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string UseNonceCookie(
    HttpContext context,
    NonceCookieBuilder nonceCookieBuilder,
    StringDataFormat stringDataFormat,
    OpenIdConnectProtocolValidator protocolValidator,
    DateTimeOffset currentUtc)
  {
    var nonce = protocolValidator.GenerateNonce();
    var nonceCookieName = GetNonceCookieName(nonceCookieBuilder.Name, stringDataFormat.Protect(nonce));
    var nonceCookieOptions = nonceCookieBuilder.Build(context, currentUtc);
    AppendNonceCookie(context.Response, nonceCookieName, nonceCookieOptions);
    return nonceCookieName;
  }
}