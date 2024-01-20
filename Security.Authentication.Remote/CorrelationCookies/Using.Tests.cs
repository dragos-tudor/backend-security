
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteTests {

  [Fact]
  public void Authentication_options_with_remote_timeout__use_correlation_cookie__cookie_expires_is_current_time_plus_remote_timeout() {
    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback", RemoteAuthenticationTimeout = TimeSpan.FromHours(1) };
    var currentUtc = DateTimeOffset.UtcNow;
    var cookieName = UseCorrelationCookie(context, "correlation.id", authOptions, currentUtc);

    var expected = (currentUtc + authOptions.RemoteAuthenticationTimeout).ToString("r");
    Assert.Contains(expected, GetResponseCookie(context.Response, cookieName));
  }

  [Fact]
  public void Authentication_options_with_callback_path__use_correlation_cookie__cookie_path_is_callback_path() {
    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback" };
    var cookieName = UseCorrelationCookie(context, "correlation.id", authOptions, DateTimeOffset.Now);

    Assert.Contains("path=/callback;", GetResponseCookie(context.Response, cookieName));
  }

}