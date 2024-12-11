
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Time.Testing;
using NSubstitute;
using System.Net.Http;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public async Task Post_authorize_fail__authenticate__result_authorization_error()
  {
    var context = CreateAuthenticationHttpContext();
    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, _, _) => "authorize error";

    var result = await AuthenticateOAuth(context, postAuthorize, default!, default!, NullLogger.Instance);
    StringAssert.Contains(result.Failure!.Message, "authorize error", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Authorization_code__authenticate__exchange_authorization_code_for_tokens_receive_code()
  {
    var context = CreateAuthenticationHttpContext();
    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, _, _) => (new AuthenticationProperties(), "code");
    ExchangeCodeForTokensFunc<OAuthOptions> exchangeCodeForTokens = async (code, _, _, _, _) => { Assert.AreEqual("code", code); return await ToTask("stop exec"); };

    await AuthenticateOAuth(context, postAuthorize, exchangeCodeForTokens, default!, NullLogger.Instance);
  }

  [TestMethod]
  public async Task Access_token__authenticate__access_user_information_receive_access_token()
  {
    var context = CreateAuthenticationHttpContext();
    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, _, _) => (new AuthenticationProperties(), "code");
    ExchangeCodeForTokensFunc<OAuthOptions> exchangeCodeForTokens = async (_, _, _, _, _) => await ToTask(new OAuthTokens("access_token"));
    AccessUserInfoFunc<OAuthOptions> accessUserInfo = async (accessToken, _, _, _) => { Assert.AreEqual("access_token", accessToken); return await ToTask("stop exec"); };

    await AuthenticateOAuth(context, postAuthorize, exchangeCodeForTokens, accessUserInfo, NullLogger.Instance);
  }

  [TestMethod]
  public async Task User_info_claims__authenticate__principal_with_claims()
  {
    var oauthOptions = CreateOAuthOptions();
    var context = CreateAuthenticationHttpContext(oauthOptions);
    Claim[] claims = [CreateClaim("a", "1", oauthOptions.SchemeName)];

    PostAuthorizeFunc<OAuthOptions> postAuthorize = (_, _, _) => (new AuthenticationProperties(), "code");
    ExchangeCodeForTokensFunc<OAuthOptions> exchangeCodeForTokens = async (_, _, _, _, _) => await ToTask(new OAuthTokens("access_token"));
    AccessUserInfoFunc<OAuthOptions> accessUserInfo = async (_, _, _, _) => await ToTask(CreateUserInfoResult(claims));

    var result = await AuthenticateOAuth(context, postAuthorize, exchangeCodeForTokens, accessUserInfo, NullLogger.Instance);
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
      Scope = [ "scope1", "scope2" ],
      ScopeSeparator = ' ',
      SchemeName = schemeName,
      TokenEndpoint = "/token",
      UserInfoEndpoint = "/userinfo"
    };

  public static HttpContext CreateAuthenticationHttpContext(OAuthOptions? oauthOptions = default)
  {
    var services = new ServiceCollection()
      .AddSingleton<TimeProvider, FakeTimeProvider>()
      .AddDataProtection($"{Environment.CurrentDirectory}/keys")
      .AddSingleton(Substitute.For<PropertiesDataFormat>(Substitute.For<IDataProtector>()))
      .AddSingleton(Substitute.For<HttpClient>())
      .AddSingleton(oauthOptions ?? CreateOAuthOptions())
      .BuildServiceProvider();
    return new DefaultHttpContext() { RequestServices = services };
  }

  static Task<T> ToTask<T>(T value) => Task.FromResult(value);

}