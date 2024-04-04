
using Microsoft.AspNetCore.Http;

namespace Security.Authentication;

partial class AuthenticationFuncs
{
  const string HeaderValueNoCache = "no-cache";
  const string HeaderValueNoCacheNoStore = "no-cache,no-store";
  const string HeaderValueEpocDate = "Thu, 01 Jan 1970 00:00:00 GMT";

  public static HttpResponse ResetResponseCacheHeaders (HttpResponse response) {
    response.Headers.CacheControl = HeaderValueNoCacheNoStore;
    response.Headers.Pragma = HeaderValueNoCache;
    response.Headers.Expires = HeaderValueEpocDate;
    return response;
  }
}