using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Security.Sample.Api;

partial class ApiFuncs
{
  static readonly string[] CorsMethods = ["GET", "POST", "PUT", "PATCH", "DELETE", "OPTIONS"];
  static readonly string[] CorsHeaders = ["Accept", "Accept-Language", "Cache-Control", "Content-Language", "Content-Type", "Pragma", "Range", "Cookie", "Set-Cookie"]; // api responses not cached

  static CorsPolicy BuildCorsPolicy(params string[] origins) =>
    new CorsPolicyBuilder()
      .WithHeaders(CorsHeaders)
      .WithMethods(CorsMethods)
      .WithOrigins(origins)
      .AllowCredentials()
      .Build();
}