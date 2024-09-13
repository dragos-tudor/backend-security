
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static string ForbidAuth<TOptions> (HttpContext context, AuthenticationProperties authProperties, TOptions authOptions, ILogger logger) where TOptions: AuthenticationOptions
  {
    var returnUri = GetAuthenticationPropertiesRedirectUri(authProperties) ?? BuildRelativeUri(context.Request);
    var forbiddenPath = BuildForbidPath(authOptions, returnUri);
    SetResponseRedirect(context.Response, forbiddenPath);

    LogForbidden(logger, authOptions.SchemeName, forbiddenPath, context.TraceIdentifier);
    return forbiddenPath;
  }
}