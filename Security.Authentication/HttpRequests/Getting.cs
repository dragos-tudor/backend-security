
using Microsoft.Extensions.Primitives;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  public const string BearerName = "Bearer ";

  static KeyValuePair<string, string[]> GetHtppRequestParam(KeyValuePair<string, StringValues> pair) => new(pair.Key, pair.Value.ToArray()!);


  public static IEnumerable<KeyValuePair<string, string[]>> GetHttpRequestFormParams(IFormCollection form) => form.Select(GetHtppRequestParam);

  public static IEnumerable<KeyValuePair<string, string[]>> GetHttpRequestQueryParams(HttpRequest request) => request.Query.Select(GetHtppRequestParam);

  public static string GetHttpRequestAuthorization(HttpRequest request) => request.Headers.Authorization.ToString();

  public static string? GetHttpRequestBearerToken(string authorization) => authorization.StartsWith(BearerName, StringComparison.Ordinal)? authorization[BearerName.Length ..]: default;

  public static IRequestCookieCollection GetHttpRequestCookies(HttpRequest request) => request.Cookies;

  public static async Task<IEnumerable<KeyValuePair<string, string[]>>> GetHttpRequestParams(HttpRequest request, CancellationToken cancellationToken = default) =>
    request switch {
      var req when IsGetHttpRequest(req) => GetHttpRequestQueryParams(request),
      var req when IsFormPostHttpRequest(req) => GetHttpRequestFormParams(await ReadHttpRequestForm(request, cancellationToken)),
      _ => []
  };
}