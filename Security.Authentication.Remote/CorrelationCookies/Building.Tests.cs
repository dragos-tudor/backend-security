
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class RemoteTests {

  [Fact]
  public void Authentication_options_with_remote_timeout__build_cookie__cookie_expires_is_current_time_plus_remote_timeout() {
    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback", RemoteAuthenticationTimeout = TimeSpan.FromHours(1) };
    var currentUtc = DateTimeOffset.UtcNow;
    var cookieOptions = BuildCorrelationCookie(context, authOptions, currentUtc);
    AppendCorrelationCookie(context.Response, GetCorrelationCookieName("correlaton.id"), cookieOptions);

    var expected = (currentUtc + authOptions.RemoteAuthenticationTimeout).ToString("r");
    Assert.Contains(expected, GetResponseCookie(context.Response, GetCorrelationCookieName("correlaton.id")));
  }

  [Fact]
  public void Authentication_options_with_callback_path__build_cookie__cookie_path_is_callback_path() {
    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback" };
    var cookieOptions = BuildCorrelationCookie(context, authOptions, DateTimeOffset.UtcNow);
    AppendCorrelationCookie(context.Response, GetCorrelationCookieName("correlaton.id"), cookieOptions);

    Assert.Contains("path=/callback;", GetResponseCookie(context.Response, GetCorrelationCookieName("correlaton.id")));
  }

}