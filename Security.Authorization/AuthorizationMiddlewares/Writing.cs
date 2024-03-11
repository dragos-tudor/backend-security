using Microsoft.AspNetCore.Http;

namespace Security.Authorization;

partial class AuthorizationFuncs
{
  static Task WriteResponse(HttpContext context, string text) =>
    context.Response.WriteAsync(text);
}