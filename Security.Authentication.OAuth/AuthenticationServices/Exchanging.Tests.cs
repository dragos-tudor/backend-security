
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Localization;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class Tests {

  [Fact]
  public async Task Token_endpoint_request_with_code__exchange_code_for_tokens__endpoint_receive_code () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, "abc", context.RequestAborted);

    Assert.Contains("code=abc", result.TokenInfo!.TokenType);
  }

  [Fact]
  public async Task Token_endpoint_request_with_redirect_uri__exchange_code_for_tokens__endpoint_receive_redirect_uri () {
    var context  = CreateHttpContext(new Uri("http://localhost"));
    var remoteClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a", }));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties(new Dictionary<string, string?>() { {CallbackUri, "http://localhost/callback"} });
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.Contains("redirect_uri=" + Uri.EscapeDataString("http://localhost/callback"), result.TokenInfo!.TokenType);
  }

  [Fact]
  public async Task Token_endpoint_request_with_client_id__exchange_code_for_tokens__endpoint_receive_client_id () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, "abc", context.RequestAborted);

    Assert.Contains("client_id=client+id", result.TokenInfo!.TokenType);
  }

  [Fact]
  public async Task Token_endpoint_request_with_client_secret__exchange_code_for_tokens__endpoint_receive_client_secret () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", (request) => JsonContent.Create(new {token_type = GetRequestMessageContent(request), access_token = "a"}));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, "abc", context.RequestAborted);

    Assert.Contains("client_secret=client+secret", result.TokenInfo!.TokenType);
  }

  [Fact]
  public async Task Token_endpoint_response_with_access_token__exchange_code_for_tokens__client_receive_access_token () {
    var context = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {access_token = "access token"}));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.Equal("access token", result.TokenInfo!.AccessToken);
  }

  [Fact]
  public async Task Token_endpoint_response_with_token_lifetime__exchange_code_for_tokens__client_receive_token_lifetime () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {expires_in = 3600, access_token = string.Empty }));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.Equal("3600", result.TokenInfo!.ExpiresIn);
  }

  [Fact]
  public async Task Token_endpoint_response_without_access_token__exchange_code_for_tokens__result_access_token_error () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {}));
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var (_, error) = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.StartsWith(AccessTokenNotFound, error);
  }

  [Fact]
  public async Task Token_endpoint_response_with_generic_error__exchange_code_for_tokens__client_receive_error () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {message = "error"}), 400);
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.StartsWith(TokenEndpointError, result.Failure);
    Assert.Contains("Status: BadRequest", result.Failure);
    Assert.Contains("Body: {\"message\":\"error\"}", result.Failure);
  }

  [Fact]
  public async Task Token_endpoint_response_with_json_error__exchange_code_for_tokens__client_receive_error () {
    var context  = CreateHttpContext();
    var remoteClient = CreateHttpClient("http://oauth", "/token", JsonContent.Create(new {error = "error", error_description = "abc" }), 400);
    var authOptions = CreateOAuthOptions(context, remoteClient);
    var authProperties = new AuthenticationProperties();
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.StartsWith(TokenEndpointError, result.Failure);
    Assert.Contains("Description=abc", result.Failure);
  }

  [Fact]
  public async Task Authentication_options_using_pkcs__exchange_code_for_tokens__endpoint_receive_code_verifier () {
    using var authServer = CreateHttpServer();
    authServer.MapPost("/token", (HttpContext context) => new {token_type = context.Request.Form[CodeVerifier][0], access_token = string.Empty} );
    await authServer.StartAsync();
    using var authClient = authServer.GetTestClient();

    var context  = CreateHttpContext();
    var authOptions = CreateOAuthOptions(context, authClient) with { TokenEndpoint = "/token", UsePkce = true };
    var authProperties = new AuthenticationProperties(new Dictionary<string, string?>() { {CodeVerifier, "code verifier"} });
    var result = await ExchangeCodeForTokensAsync(authOptions, authProperties, string.Empty, context.RequestAborted);

    Assert.Equal("code verifier", result.TokenInfo!.TokenType);
  }

}
