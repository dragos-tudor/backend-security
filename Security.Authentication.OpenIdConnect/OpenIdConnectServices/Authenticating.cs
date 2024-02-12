using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OpenIdConnect;

partial class OpenIdConnectFuncs
{
  static AuthenticationTicket? AuthenticateOidc (
    HttpContext context,
    AuthenticationProperties authProperties,
    OpenIdConnectOptions oidcOptions)
  {
    // properties.Items.Remove(OAuthConstants.CodeVerifierKey);
    // properties.Items.Remove("IdToken");
    // properties.Items.Remove("AuthorizationCode");
    return default;
  }
}