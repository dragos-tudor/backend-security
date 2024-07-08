
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static bool ExistAuthenticationProperties(AuthenticationProperties? authProperties = default) =>
    authProperties is not null;
}