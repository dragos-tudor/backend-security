
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using static System.Net.HttpStatusCode;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string? ForbidAuth<TOptions> (HttpContext context, TOptions authOptions, ILogger logger) where TOptions: AuthenticationOptions
  {
    SetResponseStatus(context, Forbidden);

    LogForbidden(logger, authOptions.SchemeName, context.TraceIdentifier);
    return default;
  }

  public static string? ForbidAuth<TOptions> (HttpContext context, TOptions authOptions, AuthenticationProperties authProperties, ILogger logger) where TOptions: AuthenticationOptions
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties!) ?? BuildRelativeUri(context.Request);
    var forbiddenPath = BuildForbidPath(authOptions, returnUri);
    SetResponseRedirect(context.Response, forbiddenPath);

    LogForbidden(logger, authOptions.SchemeName, forbiddenPath, context.TraceIdentifier);
    return forbiddenPath;
  }
}