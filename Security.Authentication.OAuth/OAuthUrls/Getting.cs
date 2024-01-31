using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthFuncs
{
  const string DefaultRedirectUri = "/";

  static string GetAuthorizationUri (OAuthOptions options, IDictionary<string, string> authorizationParams) =>
    AddQueryString(options.AuthorizationEndpoint, authorizationParams!);

  static string GetCallbackRedirectUri (AuthenticationProperties authProperties) =>
    authProperties.RedirectUri ?? DefaultRedirectUri;

  public static string GetChallengeReturnUri (HttpRequest request, AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(request);
}