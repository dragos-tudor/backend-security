
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class CookiesFuncs
{
  public static string ChallengeCookie(
    HttpContext context,
    AuthenticationProperties authProperties,
    AuthenticationCookieOptions authOptions,
    ILogger logger)
  {
    SetResponseStatus(context, HttpStatusCode.Unauthorized);

    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties!) ?? GetRelativeUri(context.Request);
    var challengePath = GetChallengePath(authOptions, returnUri);

    LogChallenged(logger, authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return challengePath;
  }
}