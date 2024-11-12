using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  internal static AuthenticationProperties CreateCookieAuthenticationProperties() => new() { AllowRefresh = true, IsPersistent = true };
}