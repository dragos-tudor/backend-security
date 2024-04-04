
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string BuildChallengePath (CookieAuthenticationOptions authOptions, string returnUri) =>
    $"{authOptions.LoginPath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";

  static string BuildForbidPath (CookieAuthenticationOptions authOptions, string returnUri) =>
    $"{authOptions.AccessDeniedPath}{QueryString.Create(authOptions.ReturnUrlParameter, returnUri)}";
}