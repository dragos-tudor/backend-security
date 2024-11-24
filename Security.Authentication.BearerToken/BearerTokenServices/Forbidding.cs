using System.Net;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  public static void ForbidBearerToken(HttpContext context, BearerTokenOptions authOptions, ILogger logger) => ForbidAuth(context, authOptions, logger);
}