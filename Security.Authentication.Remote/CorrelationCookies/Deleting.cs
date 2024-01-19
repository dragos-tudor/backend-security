
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteFuncs {

  public static void DeleteCorrelationCookie (HttpResponse response, string cookieName, CookieOptions cookieOptions) =>
    response.Cookies.Delete(cookieName, cookieOptions);

}