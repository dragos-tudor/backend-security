
namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static bool ExistsAuthenticationCookie (string? authCookie) => authCookie is not null;
}