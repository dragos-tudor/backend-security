
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class Funcs {

  static async Task RemoteAuthenticationMiddleware (
    RemoteAuthenticateFunc remoteAuthenticateFunc,
    HttpContext context,
    RequestDelegate next)
  {
    if (IsPrincipalAuthenticated(context.User) || !await remoteAuthenticateFunc(context))
      await next(context);
  }

}