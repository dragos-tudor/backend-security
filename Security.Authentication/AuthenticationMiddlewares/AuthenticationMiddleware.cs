
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  static async Task AuthenticationMiddleware (
    AuthenticateFunc authenticate,
    HttpContext context,
    RequestDelegate next)
  {
    var authenticateResult = await authenticate(context);
    if(authenticateResult.Succeeded)
      SetAuthenticationFeature(context, authenticateResult);

    SetContextUser(context, authenticateResult.Principal);
    await next(context);
  }

}