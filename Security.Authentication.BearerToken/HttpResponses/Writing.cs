using System.Text.Json.Serialization.Metadata;
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  static Task WriteJsonHttpResponse<TValue>(HttpContext context, TValue value, JsonTypeInfo<TValue> jsonTypeInfo) =>
    context.Response.WriteAsJsonAsync(value, jsonTypeInfo, default, context.RequestAborted);
}