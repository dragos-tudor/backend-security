
namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  static DateTimeOffset? GetCookieOptionsExpires(DateTimeOffset currentUtc, TimeSpan? expiresAfter) => expiresAfter is not null? currentUtc.Add(expiresAfter.Value): null;
}