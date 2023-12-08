
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication.Cookies;

partial class Funcs {

  public static AuthenticationProperties CreateAuthenticationProperties (string? redirectUri = default) =>
    new () {
      IsPersistent = true,
      AllowRefresh = true,
      RedirectUri = redirectUri
    };

}