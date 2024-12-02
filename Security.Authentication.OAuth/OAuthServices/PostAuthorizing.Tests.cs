
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
  public void Post_authorization_request_with_correlation_cookie__post_authorize__deleted_correlation_cookie()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProps = new AuthenticationProperties();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    SetAuthorizationCorrelationCookie(context, "correlation.id");
    SetAuthPropsCorrelationId(authProps, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthProps(authProps, authPropsProtector));

    var (_, _,_) = PostAuthorize(context, authOptions, authPropsProtector);
    var correlationCookie = GetResponseCookie(context.Response, GetCorrelationCookieName("correlation.id"));

    StringAssert.Contains(correlationCookie, "expires=Thu, 01 Jan 1970", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Post_authorization_request_with_correlation_id__post_authorize__deleted_correlation_id()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProps = new AuthenticationProperties();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    SetAuthorizationCorrelationCookie(context, "correlation.id");
    SetAuthPropsCorrelationId(authProps, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthProps(authProps, authPropsProtector));

    var (authProps2, _, _) = PostAuthorize(context, authOptions, authPropsProtector);
    Assert.IsNull(GetAuthPropsCorrelationId(authProps2!));
  }

  [TestMethod]
  public void Post_authorization_request_without_state__post_authorize__unprotect_state_failed_error()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProps = new AuthenticationProperties();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    SetAuthorizationQueryParams(context);
    SetAuthPropsCorrelationId(authProps, "correlation.id");

    var (_, _, error) = PostAuthorize(context, authOptions, authPropsProtector);
    Assert.AreEqual(error, UnprotectStateFailed);
  }

  [TestMethod]
  public void Post_authorization_request_without_correlation_cookie__post_authorize__correlation_cookie_error()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authProps = new AuthenticationProperties();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    SetAuthPropsCorrelationId(authProps, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthProps(authProps, authPropsProtector));

    var (_, _, error) = PostAuthorize(context, authOptions, authPropsProtector);
    StringAssert.Contains(error, "correlation cookie", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Invalid_post_authorization_request__post_authorize__authorization_code_not_found_error()
  {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var (_, _, error) = PostAuthorize(context, authOptions, authPropsProtector);

    StringAssert.Contains(error, AuthorizationCodeNotFound, StringComparison.Ordinal);
  }

  static void SetAuthorizationCorrelationCookie(HttpContext context, string correlationId) =>
    SetRequestCookies(context.Request, new RequestCookieCollection().AddCookie(GetCorrelationCookieName(correlationId), "N"));

  static IQueryCollection SetAuthorizationQueryParams(HttpContext context, string? state = "state", string? code = "code") =>
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues(code) },
      { "state", new StringValues(state) }
    });

}