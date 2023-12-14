
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  static void SetResponseCookieHeader (
    HttpResponse response,
    CookieOptions cookieOptions,
    string correlationId) =>
      AppendCorrelationCookie(
        response,
        GetCorrelationCookieName(correlationId),
        cookieOptions);

}