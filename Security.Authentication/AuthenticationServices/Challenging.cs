
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string ChallengeAuth<TOptions> (HttpContext context, AuthenticationProperties authProperties, TOptions authOptions, ILogger logger) where TOptions: AuthenticationOptions
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);
    var challengePath = BuildChallengePath(authOptions, returnUri);
    SetResponseRedirect(context.Response, challengePath);

    LogChallenged(logger, authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return challengePath;
  }
}