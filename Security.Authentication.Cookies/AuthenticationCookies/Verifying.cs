
namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool ExistAuthenticationCookie(string? authCookie) => authCookie is not null;
}