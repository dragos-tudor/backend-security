
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Security.Sample.App;

partial class AppFuncs
{
  internal const string PublicCache = "public";
  internal const string PrivateCache = "public";

  static StringValues SetHeaderCacheControl (
    IHeaderDictionary headers,
    TimeSpan expiresAfter,
    string cacheType = PublicCache) =>
      headers.CacheControl = $"{cacheType},max-age={ToSecondsString(expiresAfter)}";

  static StringValues SetHeaderExpires (
    IHeaderDictionary headers,
    DateTime expiresAt) =>
      headers.Expires = ToDayDateTimeString(expiresAt);
}