
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? ResetAuthenticationPropertiesRedirectUri (AuthenticationProperties properties) =>
    properties.RedirectUri = default;
}