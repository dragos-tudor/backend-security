
using Microsoft.AspNetCore.Http;

namespace Security.Authentication.Remote;

partial class Tests {

  [Fact]
  public void Authentication_options_with_remote_timeout__setup_cookie__cookie_expires_is_current_time_plus_remote_timeout() {
    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { RemoteAuthenticationTimeout = TimeSpan.FromHours(1) };
    var currentUtc = DateTimeOffset.UtcNow;
    var cookieName = SetupCorrelationCookie(context, authOptions, currentUtc, "correlaton.id");

    var expected = (currentUtc + authOptions.RemoteAuthenticationTimeout).ToString("r");
    Assert.Contains(expected, GetResponseCookie(context.Response, cookieName));
  }

  [Fact]
  public void Authentication_options_with_callback_path__setup_cookie__cookie_path_is_callback_path() {
    var context = new DefaultHttpContext();
    var authOptions = new RemoteAuthenticationOptions() { CallbackPath = "/callback" };
    var cookieName = SetupCorrelationCookie(context, authOptions, DateTimeOffset.UtcNow, "correlaton.id");

    Assert.Contains("path=/callback;", GetResponseCookie(context.Response, cookieName));
  }

}