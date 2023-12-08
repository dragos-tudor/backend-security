
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class Funcs {

  static void UnsetResponseCookieHeader (
    HttpResponse response,
    CookieOptions cookieOptions,
    string correlationId) =>
      DeleteCorrelationCookie(
        response,
        GetCorrelationCookieName(correlationId),
        cookieOptions);

}