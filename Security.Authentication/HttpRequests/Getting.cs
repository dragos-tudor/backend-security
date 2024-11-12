
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static IRequestCookieCollection GetRequestCookies(HttpRequest request) => request.Cookies;
}