
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public async Task Token_endpoint_request_with_code__exchange_code_for_tokens__endpoint_receive_code()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (tokens, _) = await ExchangeCodeForTokens("abc", authProps, oauthOptions, httpClient);

    StringAssert.Contains(GetTokenType(tokens!), "code=abc", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Token_endpoint_request_with_redirect_uri__exchange_code_for_tokens__endpoint_receive_redirect_uri()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a", }));
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    SetAuthPropsRedirectUriForCode(authProps, "http://localhost/callback");
    var (tokens, _) = await ExchangeCodeForTokens(string.Empty, authProps, oauthOptions, httpClient);

    StringAssert.Contains(GetTokenType(tokens!), "redirect_uri=" + Uri.EscapeDataString("http://localhost/callback"), StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Token_endpoint_request_with_client_id__exchange_code_for_tokens__endpoint_receive_client_id()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (tokens, _) = await ExchangeCodeForTokens("abc", authProps, oauthOptions, httpClient);

    StringAssert.Contains(GetTokenType(tokens!), "client_id=client+id", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Token_endpoint_request_with_client_secret__exchange_code_for_tokens__endpoint_receive_client_secret()
  {
    using var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (tokens, _) = await ExchangeCodeForTokens("abc", authProps, oauthOptions, httpClient);

    StringAssert.Contains(GetTokenType(tokens!), "client_secret=client+secret", StringComparison.Ordinal);
  }

  [TestMethod]
  public async Task Token_endpoint_response_with_access_token__exchange_code_for_tokens__client_receive_access_token()
  {
    using var endpointResponse = JsonContent.Create(new {access_token = "access token"});
    using var httpClient = CreateHttpClient("http://oauth", "/token", endpointResponse);
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (tokens, _) = await ExchangeCodeForTokens(string.Empty, authProps, oauthOptions, httpClient);

    Assert.AreEqual(GetAccessToken(tokens!), "access token");
  }

  [TestMethod]
  public async Task Token_endpoint_response_with_token_lifetime__exchange_code_for_tokens__client_receive_token_lifetime()
  {
    using var endpointResponse = JsonContent.Create(new {expires_in = 3600, access_token = "a" });
    using var httpClient = CreateHttpClient("http://oauth", "/token", endpointResponse);
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (tokens, _) = await ExchangeCodeForTokens(string.Empty, authProps, oauthOptions, httpClient);

    Assert.AreEqual(GetExpiresIn(tokens!), "3600");
  }

  [TestMethod]
  public async Task Token_endpoint_response_without_access_token__exchange_code_for_tokens__result_access_token_error()
  {
    using var endpointResponse = JsonContent.Create(new {});
    using var httpClient = CreateHttpClient("http://oauth", "/token", endpointResponse);
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (_, error) = await ExchangeCodeForTokens(string.Empty, authProps, oauthOptions, httpClient);

    Assert.AreEqual(error?.ErrorType!, AccessTokenNotFound);
  }


  [TestMethod]
  public async Task Token_endpoint_response_with_json_error__exchange_code_for_tokens__client_receive_error()
  {
    using var endpointResponse = JsonContent.Create(new {error = "abc"});
    using var httpClient = CreateHttpClient("http://oauth", "/token", endpointResponse, 400);
    var oauthOptions = CreateOAuthOptions();
    var authProps = CreateAuthProps();
    var (_, error) = await ExchangeCodeForTokens(string.Empty, authProps, oauthOptions, httpClient);

    Assert.AreEqual(error?.ErrorType, "abc");
  }

  [TestMethod]
  public async Task Authentication_options_using_pkcs__exchange_code_for_tokens__endpoint_receive_code_verifier()
  {
    using var authServer = CreateHttpServer();
    authServer.MapPost("/token", (HttpContext context) => new {token_type = context.Request.Form[OAuthParamNames.CodeVerifier][0], access_token = "a"} );
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var oauthOptions = CreateOAuthOptions() with { TokenEndpoint = "/token", UsePkce = true };
    var authProps = new AuthenticationProperties();
    SetAuthPropsCodeVerifier(authProps, "code verifier");
    var (tokens, _) = await ExchangeCodeForTokens(string.Empty, authProps, oauthOptions, authClient);

    Assert.AreEqual(GetTokenType(tokens!), "code verifier");
  }

}
