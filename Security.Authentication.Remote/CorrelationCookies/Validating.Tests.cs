
using Microsoft.AspNetCore.Http;
using Security.Testing;

namespace Security.Authentication.Remote;

partial class RemoteTests {

  [TestMethod]
  public void Request_without_correlation_cookie__validate_cookie__correlation_cookie_not_found_error() {
    var request = new DefaultHttpContext().Request;
    var validationErrors = ValidateCorrelationCookie(request, "correlaton.id");

    StringAssert.StartsWith(CorrelationCookieNotFound, validationErrors);
  }

  [TestMethod]
  public void Request_with_unmatched_correlation_cookie__validate_cookie__correlation_cookie_not_found_error() {
    var request = new DefaultHttpContext().Request;
    SetRequestCookies(request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName("correlaton.id"), "abc"));
    var validationErrors = ValidateCorrelationCookie(request, "correlaton.id");

    StringAssert.StartsWith(UnexpectedCorrelationCookieContent, validationErrors);
  }

  [TestMethod]
  public void Request_with_matched_correlation_cookie__validate_cookie__no_errors() {
    var request = new DefaultHttpContext().Request;
    SetRequestCookies(request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName("correlaton.id"), CorrelationCookieMarker));
    var validationErrors = ValidateCorrelationCookie(request, "correlaton.id");

    Assert.IsNull(validationErrors);
  }

}