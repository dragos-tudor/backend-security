
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.OAuth;

partial class OAuthBaseTests {

  [TestMethod]
  public void Authentication_options_with_remote_timeout__use_correlation_cookie__cookie_expires_is_current_time_plus_remote_timeout() {
    var context = new DefaultHttpContext();
    var authOptions = CreateTestOAuthOptions() with { CallbackPath = "/callback", AuthenticationTimeout = TimeSpan.FromHours(1) };
    var currentUtc = DateTimeOffset.UtcNow;
    var cookieName = UseCorrelationCookie(context, authOptions, "correlation.id", currentUtc);

    var expected =(currentUtc + authOptions.AuthenticationTimeout).ToString("r");
    StringAssert.Contains(GetResponseCookie(context.Response, cookieName), expected, StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_callback_path__use_correlation_cookie__cookie_path_is_callback_path() {
    var context = new DefaultHttpContext();
    var authOptions = CreateTestOAuthOptions() with { CallbackPath = "/callback" };
    var cookieName = UseCorrelationCookie(context, authOptions, "correlation.id", DateTimeOffset.Now);

    StringAssert.Contains(GetResponseCookie(context.Response, cookieName), "path=/callback;", StringComparison.Ordinal);
  }

}