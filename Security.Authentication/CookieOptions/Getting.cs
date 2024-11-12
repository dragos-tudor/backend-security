
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static DateTimeOffset? GetCookieOptionsExpires(CookieOptions cookieOptions) => cookieOptions.Expires;
}