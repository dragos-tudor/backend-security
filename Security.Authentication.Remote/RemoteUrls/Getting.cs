using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs
{
  public const string HomeUri = "/";

  public static string GetCallbackRedirectUri (AuthenticationProperties authProperties) => GetAuthenticationPropertiesRedirectUri(authProperties) ?? HomeUri;
}