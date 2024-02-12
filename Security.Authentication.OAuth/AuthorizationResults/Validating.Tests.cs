
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [TestMethod]
  public void Authorization_request_query_with_access_denied_error__validate_authorization_result__access_denied_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { AuthorizationError, new StringValues(AccessDeniedAuthorizationError) }
    });
    var error = ValidateAuthorizationResult(context);

    Assert.AreEqual(error, AccessDenied);
  }

  [TestMethod]
  public void Authorization_request_query_with_generic_error__validate_authorization_result__authorization_endpoint_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { AuthorizationError, new StringValues("other_error") },
      { AuthorizationErrorDescription, new StringValues("abc") }
    });
    var error = ValidateAuthorizationResult(context);

    StringAssert.Contains(error, AuthorizationEndpointError);
    StringAssert.Contains(error, "abc");
  }

  [TestMethod]
  public void Authorization_request_query_without_authorization_code__validate_authorization_result__authorization_code_not_found_error() {
    var context = CreateHttpContext();
    var error = ValidateAuthorizationResult(context);

    Assert.AreEqual(error, AuthorizationCodeNotFound);
  }

  [TestMethod]
  public void Authorization_request_query_without_state__validate_authorization_result__invalid_state_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues("abc") }
    });
    var error = ValidateAuthorizationResult(context);

    Assert.AreEqual(error, InvalidAuthorizationState);
  }

}