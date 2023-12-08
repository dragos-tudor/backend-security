
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class Funcs {

  public static IApplicationBuilder UseLocalAuthentication (this IApplicationBuilder builder, LocalAuthenticateFunc authenticateFunc) =>
    builder.Use((HttpContext context, RequestDelegate next) =>
      LocalAuthenticationMiddleware(authenticateFunc, context, next));

  public static IApplicationBuilder UseRemoteAuthentication (this IApplicationBuilder builder, RemoteAuthenticateFunc remoteAuthenticateFunc) =>
    builder.Use((HttpContext context, RequestDelegate next) =>
      RemoteAuthenticationMiddleware(remoteAuthenticateFunc, context, next));

}