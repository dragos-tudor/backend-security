
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string? ForbidGoogle (HttpContext context) =>
    ForbidAuth (context, ResolveRequiredService<GoogleOptions>(context), ResolveGoogleLogger(context));
}