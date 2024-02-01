
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  static string? GetCorrelationCookieContent (HttpRequest request, string cookieName) =>
    request.Cookies[cookieName];

  public static string GetCorrelationCookieName (string correlationId) =>
    $"{CorrelationCookieName}{correlationId}";

}