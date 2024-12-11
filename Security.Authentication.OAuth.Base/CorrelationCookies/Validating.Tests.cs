
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Security.Testing;

namespace Security.Authentication.OAuth;

partial class OAuthBaseTests
{
  [TestMethod]
  public void Request_with_matched_correlation_cookie__validate_cookie__no_errors()
  {
    var request = new DefaultHttpContext().Request;
    SetRequestCookies(request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName("correlaton.id"), CorrelationCookieMarker));
    var validationErrors = ValidateCorrelationCookie(request, "correlaton.id");

    Assert.IsNull(validationErrors);
  }

  [TestMethod]
  public void Request_with_unmatched_correlation_cookie__validate_cookie__unexpected_correlation_cookie_content_error()
  {
    var request = new DefaultHttpContext().Request;
    SetRequestCookies(request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName("correlaton.id"), "abc"));
    var validationErrors = ValidateCorrelationCookie(request, "correlaton.id");

    StringAssert.StartsWith(UnexpectedCorrelationCookieContent, validationErrors, StringComparison.Ordinal);
  }

  [TestMethod]
  public void Request_without_correlation_cookie__validate_cookie__correlation_cookie_not_found_error()
  {
    var request = new DefaultHttpContext().Request;
    var validationErrors = ValidateCorrelationCookie(request, "correlaton.id");

    StringAssert.StartsWith(CorrelationCookieNotFound, validationErrors, StringComparison.Ordinal);
  }

  [TestMethod]
  public void Request_with_correlation_id_and_without_correlation_cookie__validate_cookie__correlation_cookie_not_found()
  {
    var context = CreateHttpContext();
    var authProps = new AuthenticationProperties();
    SetAuthPropsCorrelationId(authProps, "id");
    var error = ValidateCorrelationCookie(context.Request, authProps);

    StringAssert.StartsWith(error, CorrelationCookieNotFound, StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_properties_without_correlation_id__validate_cookie__correlation_id_not_found_error()
  {
    var context = CreateHttpContext();
    var authProps = new AuthenticationProperties();
    var error = ValidateCorrelationCookie(context.Request, authProps);

    Assert.AreEqual(error, CorrelationIdKeyNotFound);
  }
}