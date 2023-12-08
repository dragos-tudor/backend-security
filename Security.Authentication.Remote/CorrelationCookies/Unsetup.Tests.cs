
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class Tests {

  [Fact]
  public void Request_with_correlation_cookie__unsetup_cookie__response_without_cookie() {
    var setupContext = new DefaultHttpContext();
    var setupAuthOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback" };
    SetupCorrelationCookie(setupContext, setupAuthOptions, DateTimeOffset.UtcNow, "correlaton.id");

    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback" };
    SetRequestCookiesHeader(context.Request, setupContext.Response);
    var cookieName = UnsetupCorrelationCookie(context, authOptions, "correlation.id");

    Assert.Null(GetResponseCookie(setupContext.Response, cookieName));
  }

}