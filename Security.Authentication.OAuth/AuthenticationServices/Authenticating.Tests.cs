
#pragma warning disable IDE0039
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using NSubstitute;
using static Security.Testing.Funcs;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication;
using System.Threading;

namespace Security.Authentication.OAuth;

partial class Tests {

  [Fact]
  public async Task Post_authorize_fail__authenticate__result_authorization_error () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, __) => (default, "authorize error");

    var result = await AuthenticateOAuthAsync(context, authOptions, postAuthorize, default!, default!);
    Assert.Equal("authorize error", result.Failure!.Message);
  }

  [Fact]
  public async Task Authorization_code__authenticate__exchange_authorization_code_for_tokens () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    SetAuthorizationQueryParams(context, code: "code");

    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, __) => (default, default);
    ExchangeCodeForTokensFunc<OAuthOptions> exchangeCodeForTokens = Substitute.For<ExchangeCodeForTokensFunc<OAuthOptions>>();
    exchangeCodeForTokens(Arg.Any<OAuthOptions>(), Arg.Any<AuthenticationProperties>(), "code", Arg.Any<CancellationToken>())!.Returns(ToTask(new TokenResult(default, "stop exec")));

    await AuthenticateOAuthAsync(context, authOptions, postAuthorize, exchangeCodeForTokens, default!);
    exchangeCodeForTokens.Received(1);
  }

  [Fact]
  public async Task Access_token__authenticate__access_user_information_with_access_token () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    SetAuthorizationQueryParams(context, code: "code");

    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, __) => (default, default);
    ExchangeCodeForTokensFunc<OAuthOptions> exchangeCodeForTokens = (_, __, ___, ____) => ToTask(new TokenResult(new TokenInfo() { AccessToken = "token" }, default));
    AccessUserInfoFunc<OAuthOptions> accessUserInfo = Substitute.For<AccessUserInfoFunc<OAuthOptions>>();
    accessUserInfo(Arg.Any<OAuthOptions>(), "token", Arg.Any<CancellationToken>())!.Returns(ToTask(new UserInfoResult(default, "stop exec")));

    await AuthenticateOAuthAsync(context, authOptions, postAuthorize, exchangeCodeForTokens, accessUserInfo);
    accessUserInfo.Received(1);
  }

  [Fact]
  public async Task Valid_authentication_flow__authenticate__authentication_with_expected_claims_principal () {
    var context = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context);
    var principal = CreateClaimsPrincipal("user name");
    SetAuthorizationQueryParams(context, code: "code");

    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, __) => (default, default);
    ExchangeCodeForTokensFunc<OAuthOptions> exchangeCodeForTokens = (_, __, ___, ____) => ToTask(new TokenResult(new TokenInfo() { AccessToken = "token" }, default));
    AccessUserInfoFunc<OAuthOptions> accessUserInfo = (_, __, ___) => ToTask(new UserInfoResult(principal, default));

    var result = await AuthenticateOAuthAsync(context, authOptions, postAuthorize, exchangeCodeForTokens, accessUserInfo);
    Assert.Same(principal, result.Principal);
  }



  static OAuthOptions CreateOAuthOptions (HttpContext context, HttpClient? remoteClient = default) =>
    CreateOAuthOptions<OAuthOptions>(ResolveRequiredService<IDataProtectionProvider>(context)) with {
      AccessDeniedPath = "/forbid",
      AuthorizationEndpoint = "/authorize",
      CallbackPath = "/callback",
      ClaimActions = new ClaimActionCollection(),
      ClientId = "client id",
      ClientSecret = "client secret",
      RemoteClient = remoteClient!,
      Scope = new [] { "scope1", "scope2" },
      ScopeSeparator = ' ',
      TokenEndpoint = "/token",
      UserInformationEndpoint = "/userinfo"
    };

  static Task<T> ToTask<T>(T value) => Task.FromResult(value);

}