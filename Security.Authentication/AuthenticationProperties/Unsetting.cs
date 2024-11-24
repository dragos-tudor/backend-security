
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool UnsetAuthPropsItem(AuthenticationProperties authProps, string key) => authProps.Items.Remove(key);
}