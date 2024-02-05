using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static AuthenticationProperties CreateAuthenticationProperties() =>
    new ();
}