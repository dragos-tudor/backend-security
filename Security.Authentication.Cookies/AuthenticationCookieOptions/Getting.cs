
namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static string GetAuthenticationCookieName(AuthenticationCookieOptions authOptions) => authOptions.CookieName;

  static TimeSpan? GetAuthenticationCookieExpireAfter(AuthenticationCookieOptions authOptions) => authOptions.ExpireAfter;
}