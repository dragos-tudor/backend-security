
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool IsSignInHttpRequest(HttpRequest request, AuthenticationCookieOptions authOptions) => EqualsStringOrdinal(GetHttpRequestFullPath(request), authOptions.SignInPath);

  static bool IsSignOutHttpRequest(HttpRequest request, AuthenticationCookieOptions authOptions) => EqualsStringOrdinal(GetHttpRequestFullPath(request), authOptions.SignOutPath);
}