
using System.Collections.Generic;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [TestMethod]
  public void Authentication_options_with_authorization_endpoint__challenge__authorization_path_is_authorization_endpoint() {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions() with { AuthorizationEndpoint = "http://oauth/" };
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);

    StringAssert.StartsWith(authorizationPath, "http://oauth/?", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_scope__challenge__authorization_query_contains_scope_param() {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions() with { Scope = new [] { "openid", "email" } };
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);

    StringAssert.Contains(authorizationPath, "scope=openid%20email", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_client_id__challenge__authorization_query_contains_client_id_param() {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions() with { ClientId = "clientID" };
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);

    StringAssert.Contains(authorizationPath, "client_id=clientID", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_callback_path__challenge__authorization_query_contains_redirect_uri_param() {
    var context = CreateHttpContext(new Uri("http://localhost"));
    var oauthOptions = CreateOAuthOptions() with { CallbackPath = "/callback" };
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);

    StringAssert.Contains(authorizationPath, "redirect_uri=" + Uri.EscapeDataString("http://localhost/callback"), StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_using_pkce__challenge__authorization_query_contains_code_challenge() {
    var context = CreateHttpContext(new Uri("http://localhost"));
    var oauthOptions = CreateOAuthOptions() with { UsePkce = true };
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);

    var authProps = authPropsProtector.Unprotect(QueryHelpers.ParseQuery(authorizationPath)["state"]);
    var codeChallenge = HashCodeVerifier(GetAuthPropsCodeVerifier(authProps!)!);
    StringAssert.Contains(authorizationPath, "code_challenge=" + codeChallenge, StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authorization_request_query_state__unprotect_state__contains_request_uri() {
    var context = CreateHttpContext(new Uri("http://localhost/challenge?returnUrl=/index"));
    var oauthOptions = CreateOAuthOptions();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);
    var stateParam = GetQueryStateParam(authorizationPath);
    var properties = UnprotectAuthProps(stateParam.Value!, authPropsProtector);

    StringAssert.Contains(properties!.RedirectUri,"/index", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authorization_request_correlation_cookie__validate_correlation_cookie__valid_cookie() {
    var context = CreateHttpContext();
    var oauthOptions = CreateOAuthOptions();
    var authPropsProtector = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, CreateAuthProps(), oauthOptions, DateTimeOffset.Now, authPropsProtector, NullLogger.Instance);
    var stateParam = GetQueryStateParam(authorizationPath);
    var properties = UnprotectAuthProps(stateParam.Value!, authPropsProtector)!;

    SetRequestCookiesHeader(context.Request, context.Response);
    var errors = ValidateCorrelationCookie(context.Request, properties);

    Assert.IsNull(errors);
  }

  static KeyValuePair<string, StringValues> GetQueryParam(string requestPath, string keyName) => QueryHelpers.ParseQuery(requestPath).First(param => param.Key == keyName);

  static KeyValuePair<string, StringValues> GetQueryStateParam(string requestPath) => GetQueryParam(requestPath, "state");

}