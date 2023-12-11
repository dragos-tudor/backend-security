
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Cookies;

partial class Funcs {

  public static string ChallengeCookie (
    HttpContext context,
    AuthenticationProperties authProperties,
    CookieAuthenticationOptions authOptions)
  {
    var returnUrl = GetPropertiesRedirectUriOrCurrentUri(context, authProperties);
    var challengePath = BuildChallengePath(authOptions, returnUrl);

    LogChallenged(Logger, authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return SetResponseRedirect(context.Response, challengePath);
  }

}