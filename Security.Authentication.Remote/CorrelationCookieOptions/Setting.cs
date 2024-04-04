
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  static DateTimeOffset? SetCorrelationCookieOptionsExpires (CookieOptions cookieOptions, DateTimeOffset? expires) =>
    cookieOptions.Expires = expires;

  static string? SetCorrelationCookieOptionsPath (CookieOptions cookieOptions, string? path) =>
    cookieOptions.Path = path;

}