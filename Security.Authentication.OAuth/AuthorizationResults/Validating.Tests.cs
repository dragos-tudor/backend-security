
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [Fact]
  public void Authorization_request_query_with_access_denied_error__post_authorize__access_denied_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { AuthorizationError, new StringValues(AccessDeniedAuthorizationError) }
    });
    var error = ValidateAuthorizationResult(context);

    Assert.Equal(AccessDenied, error);
  }

  [Fact]
  public void Authorization_request_query_with_generic_error__post_authorize__authorization_endpoint_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { AuthorizationError, new StringValues("other_error") },
      { AuthorizationErrorDescription, new StringValues("abc") }
    });
    var error = ValidateAuthorizationResult(context);

    Assert.Contains(AuthorizationEndpointError, error);
    Assert.Contains("abc", error);
  }

  [Fact]
  public void Authorization_request_query_without_authorization_code__post_authorize__authorization_code_not_found_error() {
    var context = CreateHttpContext();
    var error = ValidateAuthorizationResult(context);

    Assert.Equal(AuthorizationCodeNotFound, error);
  }

  [Fact]
  public void Authorization_request_query_without_state__post_authorize__invalid_state_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues("abc") }
    });
    var error = ValidateAuthorizationResult(context);

    Assert.Equal(InvalidAuthorizationState, error);
  }

  [Fact]
  public void Authentication_properties_without_correlation_id__post_authorize__correlation_id_not_found_error() {
    var context = CreateHttpContext();
    var authProperties = new AuthenticationProperties();
    var error = ValidateAuthorizationCorrelationCookie(context, authProperties);

    Assert.Equal(CorrelationIdKeyNotFound, error);
  }

  [Fact]
  public void Authentication_request_without_correlation_cookie__post_authorize__correlation_cookie_error() {
    var context = CreateHttpContext();
    var authProperties = new AuthenticationProperties();
    SetAuthenticationPropertiesCorrelationId(authProperties, "id");
    var error = ValidateAuthorizationCorrelationCookie(context, authProperties);

    Assert.StartsWith(CorrelationFailed, error);
  }

}