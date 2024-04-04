using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  internal const string BearerTokenName = "Bearer ";

  static string? GetAuthorizationBearerToken(string authorization) =>
    authorization.StartsWith(BearerTokenName, StringComparison.Ordinal)?
      authorization[BearerTokenName.Length ..]:
      default;

  static string GetRequestAuthorizationHeader(HttpRequest request) =>
    request.Headers.Authorization.ToString();
}