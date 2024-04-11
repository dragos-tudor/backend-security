
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteTests {

  [TestMethod]
  public void Request_with_correlation_cookie__clean_correlation_cookie__response_with_expired_cookie() {
    var challengeContext = new DefaultHttpContext();
    var remoteOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback", ChallengePath = "" };
    var cookieName = UseCorrelationCookie(challengeContext, "correlation.id", remoteOptions, DateTimeOffset.Now);

    var callbackContext = new DefaultHttpContext();
    SetRequestCookiesHeader(callbackContext.Request, challengeContext.Response);
    CleanCorrelationCookie(callbackContext, remoteOptions, "correlation.id");

    StringAssert.Contains(GetResponseCookie(callbackContext.Response, cookieName), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

}