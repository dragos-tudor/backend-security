
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using NSubstitute;
using System.Net.Http;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  readonly PropertiesDataFormat propertiesDataFormat = Substitute.For<PropertiesDataFormat>(Substitute.For<IDataProtector>());
  readonly HttpClient httpClient = Substitute.For<HttpClient>();

  [TestMethod]
  public async Task Post_authorize_fail__authenticate__result_authorization_error () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var postAuthorize = Substitute.For<PostAuthorizationFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs("authorize error");

    var result = await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, default!, default!);
    Assert.AreEqual("authorize error", result.Failure!.Message);
  }

  [TestMethod]
  public async Task Authorization_code__authenticate__exchange_authorization_code_for_tokens () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    SetAuthorizationQueryParams(context, code: "code");

    var postAuthorize = Substitute.For<PostAuthorizationFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs(new AuthenticationProperties());
    exchangeCodeForTokens("code", default!, default!, default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(default, "stop exec")));

    await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, exchangeCodeForTokens, default!);
    exchangeCodeForTokens.Received(1);
  }

  [TestMethod]
  public async Task Access_token__authenticate__access_user_information_with_access_token () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    SetAuthorizationQueryParams(context, code: "code");

    var postAuthorize = Substitute.For<PostAuthorizationFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    var accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs(new AuthenticationProperties());
    exchangeCodeForTokens("code", default!, default!, default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(new TokenInfo(AccessToken: "token"), default)));
    accessUserInfo("token", default!, default!)!.ReturnsForAnyArgs(ToTask(new UserInfoResult(default, "stop exec")));

    await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, exchangeCodeForTokens, accessUserInfo);
    accessUserInfo.Received(1);
  }

  [TestMethod]
  public async Task Valid_authentication_flow__authenticate__authentication_with_expected_claims_principal () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var principal = CreatePrincipal("user name");
    SetAuthorizationQueryParams(context, code: "code");

    var postAuthorize = Substitute.For<PostAuthorizationFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    var accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs(new AuthenticationProperties());
    exchangeCodeForTokens("code", default!, default!, default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(new TokenInfo(AccessToken: "token"), default)));
    accessUserInfo("token", default!, default!)!.ReturnsForAnyArgs(ToTask(new UserInfoResult(principal, default)));

    var result = await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, exchangeCodeForTokens, accessUserInfo);
    Assert.AreSame(principal, result.Principal);
  }

  static OAuthOptions CreateOAuthOptions (string schemeName = "oauth") =>
    new () {
      AccessDeniedPath = "/forbid",
      AuthorizationEndpoint = "/authorize",
      CallbackPath = "/callback",
      ChallengePath = "/challenge",
      ClientId = "client id",
      ClientSecret = "client secret",
      Scope = new [] { "scope1", "scope2" },
      ScopeSeparator = ' ',
      SchemeName = schemeName,
      TokenEndpoint = "/token",
      UserInformationEndpoint = "/userinfo"
    };

  static Task<T> ToTask<T>(T value) => Task.FromResult(value);

}