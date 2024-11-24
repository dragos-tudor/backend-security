
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? ResetAuthPropsRedirectUri(AuthenticationProperties authProps) => authProps.RedirectUri = default;
}