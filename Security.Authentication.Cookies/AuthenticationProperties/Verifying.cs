
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static bool IsAuthenticationPropertiesPersistent (AuthenticationProperties authProperties) => authProperties.IsPersistent;

}