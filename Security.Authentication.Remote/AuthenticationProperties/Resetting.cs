
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static string? ResetAuthenticationPropertiesRedirectUri (AuthenticationProperties properties) =>
    properties.RedirectUri = default;

}