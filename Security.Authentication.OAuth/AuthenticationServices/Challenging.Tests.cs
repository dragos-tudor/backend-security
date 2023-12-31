
using System.Collections.Generic;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [Fact]
  public void Authentication_options_with_authorization_endpoint__challenge__authorization_path_is_authorization_endpoint () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { AuthorizationEndpoint = "http://oauth/" };
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);

    Assert.StartsWith("http://oauth/?", authorizationPath);
  }

  [Fact]
  public void Authentication_options_with_scope__challenge__authorization_query_contains_scope_param () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { Scope = new [] { "openid", "email" } };
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);

    Assert.Contains("scope=openid%20email", authorizationPath);
  }

  [Fact]
  public void Authentication_options_with_client_id__challenge__authorization_query_contains_client_id_param () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions() with { ClientId = "clientID" };
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);

    Assert.Contains("client_id=clientID", authorizationPath);
  }

  [Fact]
  public void Authentication_options_with_callback_path__challenge__authorization_query_contains_redirect_uri_param () {
    var context = CreateHttpContext(new Uri("http://localhost"));
    var authOptions = CreateOAuthOptions() with { CallbackPath = "/callback" };
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);

    Assert.Contains("redirect_uri=" + Uri.EscapeDataString("http://localhost/callback"), authorizationPath);
  }

  [Fact]
  public void Authentication_options_using_pkce__challenge__authorization_query_contains_code_challenge () {
    var context = CreateHttpContext(new Uri("http://localhost"));
    var authOptions = CreateOAuthOptions() with { UsePkce = true };
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);

    var authProperties = secureDataFormat.Unprotect(QueryHelpers.ParseQuery(authorizationPath)["state"]);
    var codeChallenge = HashCodeVerifier(GetAuthenticationPropertiesCodeVerifier(authProperties!)!);
    Assert.Contains("code_challenge=" + codeChallenge, authorizationPath);
  }

  [Fact]
  public void Authorization_query_state__unprotect_state__contains_request_uri () {
    var context = CreateHttpContext(new Uri("http://localhost/challenge?returnUrl=/index"));
    var authOptions = CreateOAuthOptions();
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);
    var stateParam = GetQueryStateParam(authorizationPath);
    var properties = UnprotectAuthenticationProperties(stateParam.Value, secureDataFormat);

    Assert.Contains("/index", properties!.RedirectUri);
  }

  [Fact]
  public void Challenge_response_correlation_cookie__validate_correlation_cookie__valid_cookie () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var secureDataFormat = CreateStateDataFormat(ResolveService<IDataProtectionProvider>(context));
    var authorizationPath = ChallengeOAuth(context, authOptions, secureDataFormat, DateTimeOffset.Now);
    var stateParam = GetQueryStateParam(authorizationPath);
    var properties = UnprotectAuthenticationProperties(stateParam.Value, secureDataFormat)!;

    SetRequestCookiesHeader(context.Request, context.Response);
    var errors = ValidateCorrelationCookie(context.Request, GetAuthenticationPropertiesCorrelationId(properties)!);

    Assert.Null(errors);
  }

  static KeyValuePair<string, StringValues> GetQueryParam (string requestPath, string keyName) =>
    QueryHelpers.ParseQuery(requestPath).First(param => param.Key == keyName);

  static KeyValuePair<string, StringValues> GetQueryStateParam (string requestPath) =>
    GetQueryParam(requestPath, "state");

}