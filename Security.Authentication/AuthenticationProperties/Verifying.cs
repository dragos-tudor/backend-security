
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool ExistsAuthPropsRedirectUri(AuthenticationProperties authProps) => ExistsUri(GetAuthPropsRedirectUri(authProps));
}