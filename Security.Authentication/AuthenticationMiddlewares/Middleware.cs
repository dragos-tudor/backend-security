
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  static async Task AuthenticationMiddleware(AuthenticateFunc authenticate, HttpContext context, RequestDelegate next)
  {
    var authResult = await authenticate(context);
    if (authResult.Succeeded) SetAuthenticationFeature(context, authResult);

    SetContextUser(context, authResult.Principal);
    await next(context);
  }
}