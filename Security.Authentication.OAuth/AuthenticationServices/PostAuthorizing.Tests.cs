
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Security.Testing;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class Tests {

  [Fact]
  public void Invalid_authorization_request__post_authorize__result_error() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    var (_, error) = PostAuthorize(context, authOptions);

    Assert.NotEmpty(error!);
  }

  [Fact]
  public void Authorization_request_without_state__post_authorize__unprotect_state_failed_error()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    var authProperties = new AuthenticationProperties();
    SetAuthorizationQueryParams(context);
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");

    var (_, error) = PostAuthorize(context, authOptions);
    Assert.Equal(UnprotectAuthorizationStateFailed, error);
  }

  [Fact]
  public void Authorization_request_without_correlation_cookie__post_authorize__correlation_cookie_error() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    var authProperties = new AuthenticationProperties();
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthenticationProperties(authProperties, authOptions.StateDataFormat));

    var (_, error) = PostAuthorize(context, authOptions);
    Assert.Contains("Correlation cookie", error);
  }

  [Fact]
  public void Authorization_request_with_correlation_cookie__post_authorize__deleted_correlation_cookie() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    var authProperties = new AuthenticationProperties();
    SetAuthorizationCorrelationCookie(context, "correlation.id");
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthenticationProperties(authProperties, authOptions.StateDataFormat));

    var (_, _) = PostAuthorize(context, authOptions);
    var correlationCookie = GetResponseCookie(context.Response, GetCorrelationCookieName("correlation.id"));

    Assert.Contains("expires=Thu, 01 Jan 1970", correlationCookie);
  }

  [Fact]
  public void Authorization_request_with_correlation_id__post_authorize__deleted_correlation_id() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    var authProperties = new AuthenticationProperties();
    SetAuthorizationCorrelationCookie(context, "correlation.id");
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthenticationProperties(authProperties, authOptions.StateDataFormat));

    var (authProperties2, _) = PostAuthorize(context, authOptions);
    Assert.Null(GetAuthenticationPropertiesCorrelationId(authProperties2!));
  }

  static void SetAuthorizationCorrelationCookie(HttpContext context, string correlationId) =>
    SetRequestCookies(context.Request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName(correlationId), "N"));

  static IQueryCollection SetAuthorizationQueryParams(HttpContext context, string? state = "state", string? code = "code") =>
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues(code) },
      { "state", new StringValues(state) }
    });

}