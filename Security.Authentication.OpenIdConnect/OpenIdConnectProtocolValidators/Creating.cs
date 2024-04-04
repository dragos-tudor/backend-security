using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static OpenIdConnectProtocolValidator CreateOpenIdConnectProtocolValidator() =>
    new ();
}