
namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string GetCookieName (AuthenticationCookieOptions authOptions) => authOptions.CookieName ?? authOptions.SchemeName;
}