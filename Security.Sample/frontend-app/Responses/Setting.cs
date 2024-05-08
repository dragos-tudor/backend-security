
using System;
using Microsoft.AspNetCore.Http;

namespace Security.Sample.App;

partial class AppFuncs
{
  static IHeaderDictionary SetResponseCache (HttpResponse response, DateTime currentDate, TimeSpan expiresAfter)
  {
    SetHeaderCacheControl(response.Headers, expiresAfter);
    SetHeaderExpires(response.Headers, currentDate.Add(expiresAfter));
    return response.Headers;
  }
}