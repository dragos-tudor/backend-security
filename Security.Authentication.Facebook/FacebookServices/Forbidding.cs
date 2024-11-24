
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static void ForbidFacebook(HttpContext context, FacebookOptions authOptions, ILogger logger) => ForbidAuth(context, authOptions, logger);
}