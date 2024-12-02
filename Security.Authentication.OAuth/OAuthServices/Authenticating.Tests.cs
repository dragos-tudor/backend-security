
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;
using System.Net.Http;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  readonly PropertiesDataFormat authPropsProtector = Substitute.For<PropertiesDataFormat>(Substitute.For<IDataProtector>());
  readonly HttpClient httpClient = Substitute.For<HttpClient>();

  [TestMethod]
  public async Task Post_authorize_fail__authenticate__result_authorization_error() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs("authorize error");

    var result = await AuthenticateOAuth(context, authOptions, authPropsProtector, httpClient, postAuthorize, default!, default!, NullLogger.Instance);
    Assert.AreEqual("authorize error", result.Failure!.Message);
  }

  [TestMethod]
  public async Task Authorization_code__authenticate__exchange_authorization_code_for_tokens() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();

    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs(new PostAuthorizeResult(new AuthenticationProperties(), "code"));
    exchangeCodeForTokens("code", default!, default!, default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(default, "stop exec")));

    await AuthenticateOAuth(context, authOptions, authPropsProtector, httpClient, postAuthorize, exchangeCodeForTokens, default!, NullLogger.Instance);
    exchangeCodeForTokens.Received(1);
  }

  [TestMethod]
  public async Task Access_token__authenticate__access_user_information_with_access_token() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();

    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    var accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs(new PostAuthorizeResult(new AuthenticationProperties(), "code"));
    exchangeCodeForTokens("code", default!, default!, default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(new OAuthTokens(AccessToken: "token"), default)));
    accessUserInfo("token", default!, default!)!.ReturnsForAnyArgs(ToTask(new UserInfoResult(default, "stop exec")));

    await AuthenticateOAuth(context, authOptions, authPropsProtector, httpClient, postAuthorize, exchangeCodeForTokens, accessUserInfo, NullLogger.Instance);
    accessUserInfo.Received(1);
  }

  [TestMethod]
  public async Task Valid_authentication_flow__authenticate__authentication_with_expected_claims_principal() {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    Claim[] claims = [CreateClaim("a", "1", authOptions.SchemeName)];

    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    var accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs(new PostAuthorizeResult(new AuthenticationProperties(), "code"));
    exchangeCodeForTokens("code", default!, default!, default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(new OAuthTokens(AccessToken: "token"), default)));
    accessUserInfo("token", default!, default!)!.ReturnsForAnyArgs(ToTask(new UserInfoResult(claims)));

    var result = await AuthenticateOAuth(context, authOptions, authPropsProtector, httpClient, postAuthorize, exchangeCodeForTokens, accessUserInfo, NullLogger.Instance);
    CollectionAssert.AreEqual(claims.Select(c => c.ToString()).ToArray(), result.Principal!.Claims.Select(c => c.ToString()).ToArray());
  }

  static OAuthOptions CreateOAuthOptions(string schemeName = "oauth") =>
    new() {
      AuthorizationEndpoint = "/authorize",
      CallbackPath = "/callback",
      ChallengePath = "/challenge",
      ClientId = "client id",
      ClientSecret = "client secret",
      ResponseType = "code",
      Scope = new [] { "scope1", "scope2" },
      ScopeSeparator = ' ',
      SchemeName = schemeName,
      TokenEndpoint = "/token",
      UserInfoEndpoint = "/userinfo"
    };

  static Task<T> ToTask<T>(T value) => Task.FromResult(value);

}