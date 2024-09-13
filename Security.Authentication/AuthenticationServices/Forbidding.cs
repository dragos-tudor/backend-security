
using static System.Net.HttpStatusCode;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? ForbidAuth<TOptions> (HttpContext context, TOptions authOptions, ILogger logger, AuthenticationProperties? authProperties = default) where TOptions: AuthenticationOptions
  {
    if (!ExistsAuthenticationProperties(authProperties)) {
      SetResponseStatus(context, Forbidden);

      LogForbidden(logger, authOptions.SchemeName, context.TraceIdentifier);
      return default;
    }

    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties!) ?? BuildRelativeUri(context.Request);
    var forbiddenPath = BuildForbidPath(authOptions, returnUri);
    SetResponseRedirect(context.Response, forbiddenPath);

    LogForbidden(logger, authOptions.SchemeName, forbiddenPath, context.TraceIdentifier);
    return forbiddenPath;
  }
}