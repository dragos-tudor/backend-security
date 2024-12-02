
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static bool RemoveAuthPropsItem(AuthenticationProperties authProps, string key) => authProps.Items.Remove(key);
}