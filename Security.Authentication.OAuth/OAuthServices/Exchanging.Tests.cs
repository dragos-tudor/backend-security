
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests {

  [TestMethod]
  public async Task Token_endpoint_request_with_code__exchange_code_for_tokens__endpoint_receive_code () {
    var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens("abc", authProperties, authOptions, httpClient);

    StringAssert.Contains(GetTokenType(result), "code=abc");
  }

  [TestMethod]
  public async Task Token_endpoint_request_with_redirect_uri__exchange_code_for_tokens__endpoint_receive_redirect_uri () {
    var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a", }));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties(new Dictionary<string, string?>() { {CallbackUri, "http://localhost/callback"} });
    var result = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, httpClient);

    StringAssert.Contains(GetTokenType(result), "redirect_uri=" + Uri.EscapeDataString("http://localhost/callback"));
  }

  [TestMethod]
  public async Task Token_endpoint_request_with_client_id__exchange_code_for_tokens__endpoint_receive_client_id () {
    var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens("abc", authProperties, authOptions, httpClient);

    StringAssert.Contains(GetTokenType(result), "client_id=client+id");
  }

  [TestMethod]
  public async Task Token_endpoint_request_with_client_secret__exchange_code_for_tokens__endpoint_receive_client_secret () {
    var httpClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens("abc", authProperties, authOptions, httpClient);

    StringAssert.Contains(GetTokenType(result), "client_secret=client+secret");
  }

  [TestMethod]
  public async Task Token_endpoint_response_with_access_token__exchange_code_for_tokens__client_receive_access_token () {
    var httpClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {access_token = "access token"}));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, httpClient);

    Assert.AreEqual(GetAccessToken(result), "access token");
  }

  [TestMethod]
  public async Task Token_endpoint_response_with_token_lifetime__exchange_code_for_tokens__client_receive_token_lifetime () {
    var httpClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {expires_in = 3600, access_token = string.Empty }));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, httpClient);

    Assert.AreEqual(GetExpiresIn(result), "3600");
  }

  [TestMethod]
  public async Task Token_endpoint_response_without_access_token__exchange_code_for_tokens__result_access_token_error () {
    var httpClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {}));
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var (_, error) = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, httpClient);

    StringAssert.StartsWith(error, AccessTokenNotFound);
  }

  [TestMethod]
  public async Task Token_endpoint_response_with_generic_error__exchange_code_for_tokens__client_receive_error () {
    var httpClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {message = "error"}), 400);
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, httpClient);

    StringAssert.StartsWith(result.Failure, TokenEndpointError);
    StringAssert.Contains(result.Failure, "Status: BadRequest");
    StringAssert.Contains(result.Failure, "Body: {\"message\":\"error\"}");
  }

  [TestMethod]
  public async Task Token_endpoint_response_with_json_error__exchange_code_for_tokens__client_receive_error () {
    var httpClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {error = "error", error_description = "abc" }), 400);
    var authOptions = CreateOAuthOptions();
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, httpClient);

    StringAssert.StartsWith(result.Failure, TokenEndpointError);
    StringAssert.Contains(result.Failure, "Description=abc");
  }

  [TestMethod]
  public async Task Authentication_options_using_pkcs__exchange_code_for_tokens__endpoint_receive_code_verifier () {
    using var authServer = CreateHttpServer();
    authServer.MapPost("/token", (HttpContext context) => new {token_type = context.Request.Form[CodeVerifier][0], access_token = string.Empty} );
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var authOptions = CreateOAuthOptions() with { TokenEndpoint = "/token", UsePkce = true };
    var authProperties = new AuthenticationProperties();
    SetAuthenticationPropertiesCodeVerifier(authProperties, "code verifier");
    var result = await ExchangeCodeForTokens(string.Empty, authProperties, authOptions, authClient);

    Assert.AreEqual(GetTokenType(result), "code verifier");
  }

}
