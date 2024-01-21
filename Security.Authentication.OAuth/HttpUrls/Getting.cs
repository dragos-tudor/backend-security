using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string DefaultRedirectUri = "/";

  static string GetCallbackRedirectUri(AuthenticationProperties authProperties) =>
    authProperties.RedirectUri ?? DefaultRedirectUri;

  static string GetChallengeReturnUri(HttpRequest request, AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(request);
}