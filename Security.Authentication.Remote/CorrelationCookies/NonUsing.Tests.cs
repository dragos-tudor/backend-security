
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteTests {

  [Fact]
  public void Request_with_correlation_cookie__non_use_correlation_cookie__response_with_expired_cookie() {
    var challengeContext = new DefaultHttpContext();
    var remoteOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback", ChallengePath = "" };
    var cookieName = UseCorrelationCookie(challengeContext, "correlation.id", remoteOptions, DateTimeOffset.Now);

    var callbackContext = new DefaultHttpContext();
    SetRequestCookiesHeader(callbackContext.Request, challengeContext.Response);
    NonUseCorrelationCookie(callbackContext, remoteOptions, "correlation.id");

    Assert.Contains("expires=Thu, 01 Jan 1970", GetResponseCookie(callbackContext.Response, cookieName));
  }

}