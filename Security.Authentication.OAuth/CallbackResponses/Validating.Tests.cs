
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using static Security.Testing.Funcs;

namespace Security.Authentication.OAuth;

partial class OAuthTests
{
  [TestMethod]
  public void Callback_response_with_code_and_state__validate_callback_response__no_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues("abc") },
      { "state", new StringValues("state") }
    });
    var error = ValidateCallbackResponse(context.Request);

    Assert.IsNull(error);
  }

  [TestMethod]
  public void Callback_response_without_authorization_code__validate_callback_response__authorization_code_not_found_error() {
    var context = CreateHttpContext();
    var error = ValidateCallbackResponse(context.Request);

    Assert.AreEqual(error, MissingAuthorizationCode);
  }

  [TestMethod]
  public void Callback_response_without_state__validate_callback_response__invalid_state_error() {
    var context = CreateHttpContext();
    context.Request.Query = new QueryCollection(new Dictionary<string, StringValues>() {
      { "code", new StringValues("abc") }
    });
    var error = ValidateCallbackResponse(context.Request);

    Assert.AreEqual(error, MissingState);
  }
}