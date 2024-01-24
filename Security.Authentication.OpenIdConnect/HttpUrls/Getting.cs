using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static string GetAuthorizationUri(HttpResponse response, OpenIdConnectConfiguration oidcConfiguration) =>
    GetResponseLocation(response) ?? oidcConfiguration.AuthorizationEndpoint;
}