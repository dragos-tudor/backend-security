
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static void ForbidCookie(HttpContext context, AuthenticationCookieOptions authOptions, ILogger logger) => ForbidAuth(context, authOptions, logger);
}