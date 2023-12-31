using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string DefaultRedirectUri = "/";

  static string GetSigningRedirectUri(AuthenticationProperties authProperties) =>
    authProperties.RedirectUri ?? DefaultRedirectUri;
}