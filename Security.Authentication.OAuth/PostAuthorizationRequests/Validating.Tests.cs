
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [TestMethod]
  public void Post_authorization_request_query_with_access_denied_error__validate_post_authorization_request__access_denied_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { ErrorKey, new StringValues(AccessDeniedToken) }
    });
    var error = ValidatePostAuthorizationRequest(context);

    Assert.AreEqual(error, AccessDeniedError);
  }

  [TestMethod]
  public void Post_authorization_request_query_with_generic_error__validate_post_authorization_request__authorization_endpoint_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { ErrorKey, new StringValues("other_error") },
      { ErrorDescriptionToken, new StringValues("abc") }
    });
    var error = ValidatePostAuthorizationRequest(context);

    StringAssert.Contains(error, EndpointError);
    StringAssert.Contains(error, "abc");
  }

  [TestMethod]
  public void Post_authorization_request_query_without_authorization_code__validate_post_authorization_request__authorization_code_not_found_error() {
    var context = CreateHttpContext();
    var error = ValidatePostAuthorizationRequest(context);

    Assert.AreEqual(error, PostAuthorizationCodeNotFound);
  }

  [TestMethod]
  public void Post_authorization_request_query_without_state__validate_post_authorization_request__invalid_state_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues("abc") }
    });
    var error = ValidatePostAuthorizationRequest(context);

    Assert.AreEqual(error, InvalidPostAuthorizationState);
  }

}