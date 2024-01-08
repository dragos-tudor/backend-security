using Microsoft.AspNetCore.Http;

namespace Security.Authentication.BearerToken;

partial class BearerTokenFuncs
{
  const string BearerTokenName = "Bearer ";

  static string? GetRequestBearerToken(HttpRequest request)
  {
    var authorization = request.Headers.Authorization.ToString();
    return authorization.StartsWith(BearerTokenName, StringComparison.Ordinal)?
      authorization[BearerTokenName.Length ..]:
      default;
  }
}