
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using static System.Net.HttpStatusCode;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  public static string? ForbidAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties,
    TOptions authOptions
  ) where TOptions: Authentication.AuthenticationOptions
  {
    LogForbidden(Logger, authOptions.SchemeName, context.TraceIdentifier);
    SetResponseStatus(context, Forbidden);
    return default;
  }

  public static string? ForbidAuth<TOptions> (
    HttpContext context,
    AuthenticationProperties authProperties
  ) where TOptions: Authentication.AuthenticationOptions
   =>
      ForbidAuth(
        context,
        authProperties,
        ResolveService<TOptions>(context));
}