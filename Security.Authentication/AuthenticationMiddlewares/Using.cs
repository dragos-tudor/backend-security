
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public static IApplicationBuilder UseAuthentication(this IApplicationBuilder builder, AuthenticateFunc authenticate) =>
    builder.Use((HttpContext context, RequestDelegate next) =>  AuthenticationMiddleware(authenticate, context, next));
}