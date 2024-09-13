
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Google;

partial class GoogleFuncs
{
  public static string? ForbidGoogle (HttpContext context, AuthenticationProperties? authProperties = default) =>
    ForbidAuth (context, ResolveRequiredService<GoogleOptions>(context), ResolveGoogleLogger(context), authProperties);
}