using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static AuthenticationProperties CreateAuthProps(bool allowRefresh = true, bool isPersistent = true) =>
    new() { AllowRefresh = allowRefresh, IsPersistent = isPersistent };
}