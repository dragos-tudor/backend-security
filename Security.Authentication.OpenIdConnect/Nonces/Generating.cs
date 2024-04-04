
namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GenerateNonce() =>
    CreateOpenIdConnectProtocolValidator().GenerateNonce();
}