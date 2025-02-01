
using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static void ForbidAuth<TOptions>(
    HttpContext context,
    TOptions authOptions,
    ILogger logger)
  where TOptions: AuthenticationOptions
  {
    SetHttpResponseStatus(context.Response, HttpStatusCode.Forbidden);
    LogForbidden(logger, authOptions.SchemeName, GetRelativeUri(context.Request), context.TraceIdentifier);
  }
}