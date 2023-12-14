
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs {

  static bool IsRequestLoginPath (HttpRequest request, CookieAuthenticationOptions authOptions) =>
    request.Path == authOptions.LoginPath;

  static bool IsRequestLogoutPath (HttpRequest request, CookieAuthenticationOptions authOptions) =>
    request.Path == authOptions.LogoutPath;

}