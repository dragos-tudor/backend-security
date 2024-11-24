
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static void ChallengeCookie(HttpContext context, AuthenticationCookieOptions authOptions, ILogger logger) => ChallengeAuth(context, authOptions, logger);
}