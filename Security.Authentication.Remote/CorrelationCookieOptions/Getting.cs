
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  static DateTimeOffset? GetCorrelationCookieOptionsExpires (CookieOptions cookieOptions, TimeSpan expires, DateTimeOffset currentUtc) =>
    cookieOptions.Expires ?? currentUtc.Add(expires);

  static string? GetCorrelationCookieOptionsPath (HttpRequest request, string callbackPath) =>
    $"{request.PathBase}{callbackPath}";

}