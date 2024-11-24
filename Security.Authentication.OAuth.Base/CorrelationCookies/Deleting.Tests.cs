
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseTests
{
  [TestMethod]
  public void Request_with_correlation_cookie__delete_correlation_cookie__response_with_expired_cookie() {
    var challengeContext = new DefaultHttpContext();
    var authOptions = CreateTestOAuthOptions() with { CallbackPath = "/callback" };
    var cookieName = UseCorrelationCookie(challengeContext, authOptions, "correlation.id", DateTimeOffset.Now);

    var callbackContext = new DefaultHttpContext();
    SetRequestCookiesHeader(callbackContext.Request, challengeContext.Response);
    DeleteCorrelationCookie(callbackContext, authOptions, "correlation.id");

    StringAssert.Contains(GetResponseCookie(callbackContext.Response, cookieName), "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  static OAuthOptions CreateTestOAuthOptions() => new() {
    SchemeName = "", ChallengePath = "", CallbackPath = "", AuthorizationEndpoint = "",
    ClientId = "", ClientSecret = "", TokenEndpoint = "", UserInfoEndpoint = "", ResponseType = "code"
  };
}