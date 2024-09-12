
using System.Collections.Generic;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [TestMethod]
  public void Authentication_options_with_authorization_endpoint__challenge__authorization_path_is_authorization_endpoint () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { AuthorizationEndpoint = "http://oauth/" };
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);

    StringAssert.StartsWith(authorizationPath, "http://oauth/?", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_scope__challenge__authorization_query_contains_scope_param () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { Scope = new [] { "openid", "email" } };
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);

    StringAssert.Contains(authorizationPath, "scope=openid%20email", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_client_id__challenge__authorization_query_contains_client_id_param () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { ClientId = "clientID" };
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);

    StringAssert.Contains(authorizationPath, "client_id=clientID", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_with_callback_path__challenge__authorization_query_contains_redirect_uri_param () {
    var context = CreateHttpContext(new Uri("http://localhost"));
    var authOptions = CreateOAuthOptions() with { CallbackPath = "/callback" };
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);

    StringAssert.Contains(authorizationPath, "redirect_uri=" + Uri.EscapeDataString("http://localhost/callback"), StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authentication_options_using_pkce__challenge__authorization_query_contains_code_challenge () {
    var context = CreateHttpContext(new Uri("http://localhost"));
    var authOptions = CreateOAuthOptions() with { UsePkce = true };
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);

    var authProperties = propertiesDataFormat.Unprotect(QueryHelpers.ParseQuery(authorizationPath)["state"]);
    var codeChallenge = HashCodeVerifier(GetAuthenticationPropertiesCodeVerifier(authProperties!)!);
    StringAssert.Contains(authorizationPath, "code_challenge=" + codeChallenge, StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authorization_request_query_state__unprotect_state__contains_request_uri () {
    var context = CreateHttpContext(new Uri("http://localhost/challenge?returnUrl=/index"));
    var authOptions = CreateOAuthOptions();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);
    var stateParam = GetQueryStateParam(authorizationPath);
    var properties = UnprotectAuthenticationProperties(stateParam.Value!, propertiesDataFormat);

    StringAssert.Contains(properties!.RedirectUri,"/index", StringComparison.Ordinal);
  }

  [TestMethod]
  public void Authorization_request_correlation_cookie__validate_correlation_cookie__valid_cookie () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var propertiesDataFormat = CreatePropertiesDataFormat(ResolveRequiredService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, propertiesDataFormat, DateTimeOffset.Now);
    var stateParam = GetQueryStateParam(authorizationPath);
    var properties = UnprotectAuthenticationProperties(stateParam.Value!, propertiesDataFormat)!;

    SetRequestCookiesHeader(context.Request, context.Response);
    var errors = ValidateCorrelationCookie(context.Request, properties);

    Assert.IsNull(errors);
  }

  static KeyValuePair<string, StringValues> GetQueryParam (string requestPath, string keyName) => QueryHelpers.ParseQuery(requestPath).First(param => param.Key == keyName);

  static KeyValuePair<string, StringValues> GetQueryStateParam (string requestPath) => GetQueryParam(requestPath, "state");

}