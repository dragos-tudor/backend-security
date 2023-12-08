
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class Funcs {

  static bool IsAuthenticationPropertiesPersistent (AuthenticationProperties authProperties) => authProperties.IsPersistent;

}