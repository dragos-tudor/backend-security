
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public void Post_authorization_request_without_state__post_authorize__unprotect_state_failed_error()
  {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions();
    var authProps = new AuthenticationProperties();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    SetAuthorizationQueryParams(context);
    SetAuthPropsCorrelationId(authProps, "correlation.id");

    var (_, _, error) = PostAuthorize(context, oauthOptions, authPropsProtector);
    Assert.AreEqual(error?.ErrorType, UnprotectStateFailed);
  }

  [TestMethod]
  public void Post_authorization_request_without_correlation_cookie__post_authorize__correlation_cookie_error()
  {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions();
    var authProps = new AuthenticationProperties();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    SetAuthPropsCorrelationId(authProps, "correlation.id");
    SetAuthorizationQueryParams(context, ProtectAuthProps(authProps, authPropsProtector));

    var (_, _, error) = PostAuthorize(context, oauthOptions, authPropsProtector);
    StringAssert.Contains(error?.ErrorType, "correlation cookie", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Invalid_post_authorization_request__post_authorize__authorization_code_not_found_error()
  {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var (_, _, error) = PostAuthorize(context, oauthOptions, authPropsProtector);

    StringAssert.Contains(error?.ErrorType, MissingAuthorizationCode, StringComparison.Ordinal);
  }

  static IQueryCollection SetAuthorizationQueryParams(HttpContext context, string? state = "state", string? code = "code") =>
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues(code) },
      { "state", new StringValues(state) }
    });

}