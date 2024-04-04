using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public const string HomeUri = "/";

  public static string GetCallbackRedirectUri (AuthenticationProperties? authProperties) =>
    authProperties is not null?
      GetAuthenticationPropertiesRedirectUri(authProperties) ?? HomeUri:
      HomeUri;

  public static string GetChallengeReturnUri (HttpRequest request, AuthenticationProperties authProperties) =>
    GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(request);
}