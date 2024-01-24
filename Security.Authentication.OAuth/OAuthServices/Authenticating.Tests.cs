
#pragma warning disable IDE0039
using NSubstitute;
using Microsoft.AspNetCore.Authentication;
using System.Net.Http;
using static Security.Testing.Funcs;
using Microsoft.AspNetCore.DataProtection;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  readonly PropertiesDataFormat propertiesDataFormat = Substitute.For<PropertiesDataFormat>(Substitute.For<IDataProtector>());
  readonly HttpClient httpClient = Substitute.For<HttpClient>();

  [Fact]
  public async Task Post_authorize_fail__authenticate__result_authorization_error () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs((default, "authorize error"));

    var result = await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, default!, default!);
    Assert.Equal("authorize error", result.Failure!.Message);
  }

  [Fact]
  public async Task Authorization_code__authenticate__exchange_authorization_code_for_tokens () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    SetAuthorizationQueryParams(context, code: "code");

    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs((new AuthenticationProperties(), default));
    exchangeCodeForTokens(default!, default!, "code", default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(default, "stop exec")));

    await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, exchangeCodeForTokens, default!);
    exchangeCodeForTokens.Received(1);
  }

  [Fact]
  public async Task Access_token__authenticate__access_user_information_with_access_token () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    SetAuthorizationQueryParams(context, code: "code");

    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    var accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs((new AuthenticationProperties(), default));
    exchangeCodeForTokens(default!, default!, "code", default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(new TokenInfo(AccessToken: "token"), default)));
    accessUserInfo(default!, "token", default!)!.ReturnsForAnyArgs(ToTask(new UserInfoResult(default, "stop exec")));

    await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, exchangeCodeForTokens, accessUserInfo);
    accessUserInfo.Received(1);
  }

  [Fact]
  public async Task Valid_authentication_flow__authenticate__authentication_with_expected_claims_principal () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions();
    var principal = CreatePrincipal("user name");
    SetAuthorizationQueryParams(context, code: "code");

    var postAuthorize = Substitute.For<PostAuthorizeFunc<OAuthOptions>>();
    var exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    var accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    postAuthorize(default!, default!, default!).ReturnsForAnyArgs((new AuthenticationProperties(), default));
    exchangeCodeForTokens(default!, default!, "code", default!)!.ReturnsForAnyArgs(ToTask(new TokenResult(new TokenInfo(AccessToken: "token"), default)));
    accessUserInfo(default!, "token", default!)!.ReturnsForAnyArgs(ToTask(new UserInfoResult(principal, default)));

    var result = await AuthenticateOAuth(context, authOptions, propertiesDataFormat, httpClient, postAuthorize, exchangeCodeForTokens, accessUserInfo);
    Assert.Same(principal, result.Principal);
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