
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static string BuildChallengePath (CookieAuthenticationOptions authOptions, string returnUrl) =>
    authOptions.LoginPath + QueryString.Create(authOptions.ReturnUrlParameter, returnUrl);

  static string BuildForbidPath (CookieAuthenticationOptions authOptions, string returnUrl) =>
    authOptions.AccessDeniedPath + QueryString.Create(authOptions.ReturnUrlParameter, returnUrl);

}