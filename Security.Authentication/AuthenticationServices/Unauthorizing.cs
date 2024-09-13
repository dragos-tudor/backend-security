
using Microsoft.AspNetCore.Http;
using static System.Net.HttpStatusCode;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static int UnauthorizeAuth<TOptions> (HttpContext context, TOptions authOptions, ILogger logger) where TOptions: AuthenticationOptions
  {
    SetResponseStatus(context, Forbidden);
    LogForbidden(logger, authOptions.SchemeName, context.TraceIdentifier);
    return (int)Forbidden;
  }
}