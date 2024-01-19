
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteTests {

  [Fact]
  public void Request_with_correlation_cookie__reset_and_delete_cookie__response_wit_expired_cookie() {
    var challengeContext = new DefaultHttpContext();
    var challengeAuthOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback" };
    var challengeCookieOptions = BuildCorrelationCookie(challengeContext, challengeAuthOptions, DateTimeOffset.UtcNow);
    AppendCorrelationCookie(challengeContext.Response, GetCorrelationCookieName("correlation.id"), challengeCookieOptions);

    var callbackContext = new DefaultHttpContext();
    SetRequestCookiesHeader(callbackContext.Request, challengeContext.Response);
    var callbackCookieOptions = ResetCorrelationCookie(callbackContext, challengeAuthOptions);
    DeleteCorrelationCookie(callbackContext.Response, GetCorrelationCookieName("correlation.id"), callbackCookieOptions);

    Assert.Contains("expires=Thu, 01 Jan 1970", GetResponseCookie(callbackContext.Response, GetCorrelationCookieName("correlation.id")));
  }

}