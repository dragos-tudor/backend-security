
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  static AuthenticationProperties? GetAutheticationProperties (PostAuthorizationResult authResult) =>
    authResult.Success;
}