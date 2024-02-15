using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  const string DefaultRedirectUri = "/";

  public static string GetChallengeReturnUri (HttpRequest request, AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(request);

  public static string GetDefaultCallbackRedirectUri (AuthenticationProperties authProperties) =>
    authProperties.RedirectUri ?? DefaultRedirectUri;
}