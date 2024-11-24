
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Security.Authentication.Facebook;

partial class FacebookFuncs
{
  public static void ForbidFacebook(HttpContext context) =>
    ForbidFacebook(
      context,
      ResolveRequiredService<FacebookOptions>(context),
      ResolveFacebookLogger(context));
}