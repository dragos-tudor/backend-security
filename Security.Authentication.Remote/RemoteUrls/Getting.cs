using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public static string GetChallengeReturnUri(
    HttpRequest request,
    AuthenticationProperties authProperties) =>
      GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(request);
}