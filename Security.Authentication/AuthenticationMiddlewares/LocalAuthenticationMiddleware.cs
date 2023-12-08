
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class Funcs {

  static Task LocalAuthenticationMiddleware (
    LocalAuthenticateFunc authenticateFunc,
    HttpContext context,
    RequestDelegate next)
  {
    SetContextUser(context, authenticateFunc(context).Principal);
    return next(context);
  }

}