
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using static System.Net.HttpStatusCode;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? ChallengeAuth<TOptions> (HttpContext context, TOptions authOptions, ILogger logger) where TOptions: AuthenticationOptions
  {
    SetResponseStatus(context, Unauthorized);

    LogChallenged(logger, authOptions.SchemeName, context.TraceIdentifier);
    return default;
  }

  public static string? ChallengeAuth<TOptions> (HttpContext context, TOptions authOptions, AuthenticationProperties authProperties, ILogger logger) where TOptions: AuthenticationOptions
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties!) ?? BuildRelativeUri(context.Request);
    var challengePath = BuildChallengePath(authOptions, returnUri);
    SetResponseRedirect(context.Response, challengePath);

    LogChallenged(logger, authOptions.SchemeName, challengePath, context.TraceIdentifier);
    return challengePath;
  }
}