
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Security.Testing;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public void Post_authorization_request_with_correlation_cookie__post_authorization__deleted_correlation_cookie() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveService<IDataProtectionProvider>(context));
    SetAuthorizationCorrelationCookie(context, "correlation.id");
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    var (_, _) = PostAuthorization(context, authOptions, propertiesDataFormat);
    var correlationCookie = GetResponseCookie(context.Response, GetCorrelationCookieName("correlation.id"));

    StringAssert.Contains(correlationCookie, "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Post_authorization_request_with_correlation_id__post_authorization__deleted_correlation_id() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveService<IDataProtectionProvider>(context));
    SetAuthorizationCorrelationCookie(context, "correlation.id");
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    var (authProperties2, _) = PostAuthorization(context, authOptions, propertiesDataFormat);
    Assert.IsNull(GetAuthenticationPropertiesCorrelationId(authProperties2!));
  }

  [TestMethod]
  public void Post_authorization_request_without_state__post_authorization__unprotect_state_failed_error()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveService<IDataProtectionProvider>(context));
    SetAuthorizationQueryParams(context);
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");

    var (_, error) = PostAuthorization(context, authOptions, propertiesDataFormat);
    Assert.AreEqual(error, UnprotectAuthorizationStateFailed);
  }

  [TestMethod]
  public void Post_authorization_request_without_correlation_cookie__post_authorization__correlation_cookie_error() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveService<IDataProtectionProvider>(context));
    SetAuthenticationPropertiesCorrelationId(authProperties, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthenticationProperties(authProperties, propertiesDataFormat));

    var (_, error) = PostAuthorization(context, authOptions, propertiesDataFormat);
    StringAssert.Contains(error, "Correlation cookie", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Invalid_post_authorization_request__post_authorization__authorization_code_not_found_error() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveService<IDataProtectionProvider>(context));
    var (_, error) = PostAuthorization(context, authOptions, propertiesDataFormat);

    StringAssert.Contains(error, PostAuthorizationCodeNotFound, StringComparison.Ordinal);
  }

  static void SetAuthorizationCorrelationCookie(HttpContext context, string correlationId) =>
    SetRequestCookies(context.Request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName(correlationId), "N"));

  static IQueryCollection SetAuthorizationQueryParams(HttpContext context, string? state = "state", string? code = "code") =>
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues(code) },
      { "state", new StringValues(state) }
    });

}