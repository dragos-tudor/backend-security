
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs {

  static Task AuthenticationMiddleware (
    AuthenticateFunc authenticate,
    HttpContext context,
    RequestDelegate next)
  {
    SetContextUser(context, authenticate(context).Principal);
    return next(context);
  }

}