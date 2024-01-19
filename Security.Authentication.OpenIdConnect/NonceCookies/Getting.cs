using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GetNonceCookieName(
    NonceCookieBuilder cookieBuilder,
    OpenIdConnectProtocolValidator protocolValidator,
    StringDataFormat stringDataFormat)
  =>
    $"{cookieBuilder.Name}{stringDataFormat.Protect(protocolValidator.GenerateNonce())}";
}